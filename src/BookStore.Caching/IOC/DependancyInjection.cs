using BookStore.Caching.Factories;
using BookStore.Caching.Interfaces;
using BookStore.Caching.Repositories;
using BookStore.Caching.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Collections.Generic;

namespace BookStore.Caching.IOC
{
    public static class DependancyInjection
    {
        public static void UseRedisCaching(this IServiceCollection serviceCollection, IEnumerable<string> hosts, uint dataBase)
        {
            serviceCollection.AddSingleton(RedisConnectionFactory.BuildConnectionMultiplexer(hosts));
            serviceCollection.AddTransient<ICacheStore>(sp =>
            new RedisCacheStore(sp.GetService<IConnectionMultiplexer>(), (int)dataBase));
            serviceCollection.AddTransient<ICacheService, CacheService>();
        }

    }
}
