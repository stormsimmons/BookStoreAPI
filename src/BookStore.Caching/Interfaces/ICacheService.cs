using System;
using System.Threading.Tasks;

namespace BookStore.Caching.Interfaces
{
    public interface ICacheService
    {
        Task<T> OptimeseWithCache<T>(string cacheKey, Func<Task<T>> resultFunc, uint hoursToLive = 1);
    }
}
