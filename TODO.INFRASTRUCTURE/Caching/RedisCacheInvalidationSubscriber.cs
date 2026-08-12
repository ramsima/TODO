using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
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
    public class RedisCacheInvalidationSubscriber(IConnectionMultiplexer _redis, IMemoryCache _memoryCache) : BackgroundService
    {

        private const string ChannelName = "cache-invalidation";
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _redis.GetSubscriber();

            await subscriber.SubscribeAsync(RedisChannel.Literal(ChannelName), (channel, message) =>
            {
                var cacheKey = JsonSerializer.Deserialize<IReadOnlyCollection<string>>(message.ToString());

                if(cacheKey == null || cacheKey.Count == 0)
                {
                    return;
                }

                foreach(var key in cacheKey)
                {
                    _memoryCache.Remove(key);
                }

            });

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
