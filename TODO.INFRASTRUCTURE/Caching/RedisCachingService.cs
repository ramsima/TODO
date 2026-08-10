using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.INFRASTRUCTURE.Caching
{
    public class RedisCachingService(IConnectionMultiplexer _redis,ILogger<RedisCachingService> _logger) : ICachingService
    {

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            try
            {
                var database = _redis.GetDatabase();

                var value = await database.StringGetAsync(key);

                if (value.IsNullOrEmpty)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(value.ToString(), JsonOptions);
            }
            catch(RedisException ex)
            {
                _logger.LogWarning(
                ex,
                "Redis GET failed for cache key {CacheKey}",
                key);

                return default;
            }
            catch(JsonException ex)
            {
                _logger.LogWarning("Invalid cached JSON for key {CacheKey}", key);
                return default;
            }
            
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var cacheValue = await GetAsync<T>(key, cancellationToken);

            if(cacheValue is not null)
            {
                return cacheValue;
            }

            var value = await factory();

            await SetAsync<T>(key, value, expiration, cancellationToken);

            return value;


        }

        public async Task RemoveAsync<T>(string key, CancellationToken cancellationToken)
        {
            try
            {
                var database = _redis.GetDatabase();

                await database.KeyDeleteAsync(key);
            }
            catch(RedisException ex)
            {
                _logger.LogWarning(
                ex,
                "Redis REMOVE failed for cache key {CacheKey}",
                key);
            }
            
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var database = _redis.GetDatabase();

                var json = JsonSerializer.Serialize(value, JsonOptions);

                Expiration redisExpiration = expiration.HasValue ? new Expiration(expiration.Value) : default;

                await database.StringSetAsync(key, json, redisExpiration);
            }
            catch(RedisException ex)
            {
                _logger.LogWarning(
                ex,
                "Redis SET failed for cache key {CacheKey}",
                key);
            }
            
        }
    }
}
