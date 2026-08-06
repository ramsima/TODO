using System.Net;
using System.Text.Json;
using TODO.DOMAIN;
using TODO.APPLICATION.Common.Exceptions;
using FluentValidation;


namespace TODO.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException)
            {
                context.Response.StatusCode = 499;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }


        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                return;
            }
            var traceId = context.TraceIdentifier;
            //var (statusCode, errorcode) = ex switch
            //{
            //    NotFoundException => (HttpStatusCode.NotFound, "97"),
            //    DuplicateFieldException => (HttpStatusCode.Conflict, "97"),
            //    ConflictException => (HttpStatusCode.Conflict, "97"),
            //    ForbiddenException => (HttpStatusCode.Forbidden, "97"),
            //    BadRequestException => (HttpStatusCode.BadRequest, "97"),
            //    UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "97"),
            //    ArgumentException => (HttpStatusCode.BadRequest, "97"),
            //    _ => (HttpStatusCode.InternalServerError, "97")
            //};

            var statusCode = ex switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                DuplicateFieldException => HttpStatusCode.Conflict,
                ConflictException => HttpStatusCode.Conflict,
                ForbiddenException => HttpStatusCode.Forbidden,
                BadRequestException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var errorcode = ex is DomainException domainException
            ? domainException.ErrorCode
            : "INTERNAL_SERVER_ERROR";

            if ((int)statusCode >= 500)
                _logger.LogError(ex,
                    "Server error | TraceId: {TraceId} | {Method} {Path}",
                    traceId, context.Request.Method, context.Request.Path);
            else
                _logger.LogWarning(ex,
                    "Client error {StatusCode} | TraceId: {TraceId} | {Method} {Path}",
                    (int)statusCode, traceId, context.Request.Method, context.Request.Path);


            var message = _env.IsDevelopment() ? ex.Message : GetFriendlyMessage(ex);

            

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var response = new ApiResponse<object>
            {
                responseCode = errorcode,
                message = message,
                result = null!,
                meta = ex is ValidationException validationException ? 
                new
                {
                    traceId,
                    errors = validationException.Errors.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key,g => g.Select(e => e.ErrorMessage).ToArray())
                }:
                new
                { 
                    traceId 
                }
            };

            var json = JsonSerializer.Serialize(response, JsonOptions);
            await context.Response.WriteAsync(json);

        }

        //private static string GetFriendlyMessage(HttpStatusCode code) => code switch
        //{
        //    HttpStatusCode.NotFound => "The requested resource was not found.",
        //    HttpStatusCode.Conflict => "A conflict occurred with the current state.",
        //    HttpStatusCode.Forbidden => "You do not have permission to perform this action.",
        //    HttpStatusCode.BadRequest => "Invalid request.",
        //    HttpStatusCode.Unauthorized => "Authentication is required.",
        //    _ => "An unexpected error occurred. Please try again later."
        //};

        private static string GetFriendlyMessage(Exception ex) => ex switch
        {
            NotFoundException => "The requested resource was not found.",
            DuplicateFieldException dfe => dfe.Message,
            ConflictException => "A conflict occurred with the current state.",
            ForbiddenException => "You do not have permission to perform this action.",
            BadRequestException bre => bre.Message,
            UnauthorizedAccessException => "Authentication is required.",
            ArgumentException => "Invalid input provided.",
            _ => "An unexpected error occurred. Please try again later."

        };
    }
}
