using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Infrastructure.Repositories;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;
using TODO.APPLICATION.Interfaces;
using TODO.INFRASTRUCTURE.Caching;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using TODO.APPLICATION.Data_Interface;

namespace Todo.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IDbConnectionFactory,DbConnectionFactory>();

            var redisconnection = configuration.GetConnectionString("Redis");

            if (string.IsNullOrWhiteSpace(redisconnection))
            {
                throw new InvalidOperationException(
                    "Redis connection string is not configured.");
            }

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(redisconnection!)
                );

           

            services.AddMemoryCache();

            //services.AddScoped<ICachingService, MemoryCachingService>();

            services.AddScoped<RedisCachingService>();

            services.AddScoped<ICachingService,HybridCachingService>();

            services.AddSingleton<ICacheInvalidationPublisher, RedisCacheInvalidationPublisher>();

            services.AddHostedService<RedisCacheInvalidationSubscriber>();

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }
    }
}
