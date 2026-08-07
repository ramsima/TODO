using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Behaviors
{
    public class LoggingBehavior<TRequest,TResponse>(ILogger<LoggingBehavior<TRequest,TResponse>> _logger) : IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
    {
        private const long SlowRequestThresholdMs = 500;
        public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Handling request {RequestName}",requestName);

            var stopwatch = Stopwatch.StartNew();


            try
            {
                var response = await next();
                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > SlowRequestThresholdMs)
                {
                    _logger.LogWarning(
                        "Handled SLOW request {RequestName} in {ElapsedMilliseconds} ms",
                        requestName,
                        stopwatch.ElapsedMilliseconds);
                }
                else
                {
                    _logger.LogInformation(
                        "Handled request {RequestName} in {ElapsedMilliseconds} ms",
                        requestName,
                        stopwatch.ElapsedMilliseconds);
                }

                return response;
            }
            catch (ValidationException)
            {
                stopwatch.Stop();

                _logger.LogWarning(
                    "Validation failed for request {RequestName} after {ElapsedMilliseconds} ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }

            catch (Exception)
            {
                stopwatch.Stop();

                _logger.LogWarning(
                    
                    "Request {RequestName} failed after {ElapsedMilliseconds} ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
            
        }
    }
}
