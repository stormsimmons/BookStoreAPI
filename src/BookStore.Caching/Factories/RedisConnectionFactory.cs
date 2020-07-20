using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Caching.Factories
{
    public static class RedisConnectionFactory
    {

        public static IConnectionMultiplexer BuildConnectionMultiplexer(IEnumerable<string> hosts)
        {
            return ConnectionMultiplexer.Connect(string.Join(",", hosts));
        }
    }
}
