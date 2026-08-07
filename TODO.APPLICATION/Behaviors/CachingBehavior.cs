using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Behaviors
{
    public class CachingBehavior<TRequest,TResponse>(ICachingService _cache, ILogger<CachingBehavior<TRequest,TResponse>> _logger) : IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            if (request is not ICacheableQuery<TResponse> cacheableQuery)
            {
                return await next();
            }

            _logger.LogInformation(
                "Checking cache for {RequestName} with key {CacheKey}",
                typeof(TRequest).Name,
                cacheableQuery.CacheKey);

            return await _cache.GetOrCreateAsync(cacheableQuery.CacheKey,() => next(),cacheableQuery.Expiration,cancellationToken);
        }
    }
}
