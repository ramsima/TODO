using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Behaviors
{
    public class CacheInvalidationBehavior<TRequest,TResponse>(ICachingService _cache, ILogger<CacheInvalidationBehavior<TRequest,TResponse>> _logger,ICacheInvalidationPublisher _publisher) : IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {

            var response = await next();


            if(request is ICacheInvalidationCommand<TResponse> cacheInvalidation)
            {
                foreach(var key in cacheInvalidation.CacheKey)
                {
                    await _cache.RemoveAsync(key, cancellationToken);
                    

                }

                await _publisher.PublishAsync(cacheInvalidation.CacheKey, cancellationToken);

                _logger.LogInformation(
                    "Deleted cache for {RequestName} with key {CacheKey}",
                    typeof(TRequest).Name,
                    cacheInvalidation.CacheKey);





            }

            return response;
        }
    }
}
