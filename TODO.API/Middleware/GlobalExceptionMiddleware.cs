using System.Net;
using System.Text.Json;
using TODO.DOMAIN;
using TODO.APPLICATION.Common.Exceptions;
using FluentValidation;


namespace TODO.API.Middleware
{
    public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> _logger,RequestDelegate _next)
    {

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

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
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogInformation("An Unhandled Exception Occurred!");
                return;
            }

            var traceid = context.TraceIdentifier;

            var statuscode = ex switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                ForbiddenException => HttpStatusCode.Forbidden,
                ConflictException => HttpStatusCode.Conflict,
                DuplicateFieldException => HttpStatusCode.Conflict,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            if((int)statuscode >= 500)
            {
                _logger.LogError(ex,"Server Error | Trace Id: {Traceid} | Method: {Method} | Path: {path}",
                        traceid,context.Request.Method,context.Request.Path
                    );
            }

            else
            {
                _logger.LogWarning(ex, "Client Error | Trace Id: {Traceid} | Method: {Method} | Path: {path}",
                        traceid, context.Request.Method, context.Request.Path
                    );
            }

            var errorcode = ex switch
            {
                ValidationException => "VALIDATION_ERROR",
                DomainException domainException => domainException.ErrorCode,
                _ => "INTERNAL_SERVER_ERROR"
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statuscode;

            var response = new ApiResponse<object> {
                responseCode = errorcode,
                result = null,
                message = ex.Message,
                meta = ex is ValidationException validationException?
                        new
                        {
                            traceid,
                            errors = validationException.Errors.GroupBy(x=>x.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage)).ToArray()
                        }
                        :
                        new
                        {
                            traceid
                        }

            };

            var json = JsonSerializer.Serialize(response, JsonOptions);
            await context.Response.WriteAsync(json);
        }


    }
}
