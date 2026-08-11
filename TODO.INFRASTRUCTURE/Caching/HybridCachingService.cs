using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.INFRASTRUCTURE.Caching
{
    public class HybridCachingService(IMemoryCache _memoryCache, RedisCachingService _redisCache, ILogger<HybridCachingService> _logger) : ICachingService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            //L1 - Memory

            if(_memoryCache.TryGetValue(key,out T? memoryValue))
            {
                return memoryValue;
            }

            //L2 - Redis

            var redisCache = await _redisCache.GetAsync<T>(key, cancellationToken);

            if(redisCache is null)
            {
                return default;
            }

            _memoryCache.Set(key, redisCache);

            return redisCache;
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var cachedValue = await GetAsync<T>(key, cancellationToken);

            if(cachedValue is not null)
            {
                return cachedValue;
            }

            var value = await factory();

            await SetAsync<T>(key,value, expiration,cancellationToken);

            return value;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            //L1 - Memory

            _memoryCache.Remove(key);

            //L2 - Redis

            await _redisCache.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            //L1 - Memory

            _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration,
                Priority = CacheItemPriority.Normal
            });

            // L2 - Redis
            await _redisCache.SetAsync(key,value,expiration,cancellationToken);
        }
    }
}
