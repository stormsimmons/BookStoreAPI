using BookStore.Caching.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace BookStore.Caching.Repositories
{
    public class RedisCacheStore : ICacheStore
    {
        private IDatabase _redisDatabase;
        public RedisCacheStore(IConnectionMultiplexer connectionMultiplexer, int db)
        {
            _redisDatabase = connectionMultiplexer.GetDatabase(db);
        }

        public async Task<T> GetValueAsync<T>(string key)
        {

            var value = await _redisDatabase.StringGetAsync(key);
            if (value.IsNull)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task GetSetAsync<T>(string key, T value, uint hoursToLive)
        {
            var valueString = JsonConvert.SerializeObject(value);
            await _redisDatabase.StringSetAsync(key, valueString, TimeSpan.FromHours(hoursToLive));
        }
    }
}
