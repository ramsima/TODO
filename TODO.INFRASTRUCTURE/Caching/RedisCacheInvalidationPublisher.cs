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
    public class RedisCacheInvalidationPublisher(IConnectionMultiplexer _redis) : ICacheInvalidationPublisher
    {

        private const string ChannelName = "cache-invalidation";
        public async Task PublishAsync(IReadOnlyCollection<string> cacheKeys, CancellationToken cancellationToken = default)
        {

            if (cacheKeys.Count == 0)
            {
                return;
            }
            var subscriber = _redis.GetSubscriber();

            var message = JsonSerializer.Serialize(cacheKeys);

            await subscriber.PublishAsync(RedisChannel.Literal(ChannelName), message);
        }
    }
}
