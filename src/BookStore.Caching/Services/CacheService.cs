using BookStore.Caching.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStore.Caching.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheStore _cacheStore;

        public CacheService(ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        public async Task<T> OptimeseWithCache<T>(string cacheKey, Func<Task<T>> resultFunc, uint hoursToLive = 1)
        {
            var cachedValue = await _cacheStore.GetValueAsync<T>(cacheKey);

            if (cachedValue != null)
            {
                return cachedValue;
            }
            var result = await resultFunc();

            // Fire and forget
            _cacheStore.GetSetAsync(cacheKey, result, hoursToLive).ConfigureAwait(false);

            return result;
        }

    }
}
