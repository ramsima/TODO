using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.INFRASTRUCTURE.Caching
{
    public class MemoryCachingService(IMemoryCache _cache) : ICachingService
    {
        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            _cache.TryGetValue(key, out T? value);
            
            return Task.FromResult(value);
            



        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var cachedValue = await GetAsync<T>(key,cancellationToken);

            if (cachedValue is not null)
            {
                return cachedValue;
            }

            var value = await factory();

            if (value is not null)
            {
                await SetAsync(key,value,expiration,cancellationToken);
            }

            return value;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration,
                Priority = CacheItemPriority.Normal
                
            });

            return Task.CompletedTask;
        }
    }
}
