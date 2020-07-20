using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Caching.Interfaces
{
    public interface ICacheStore
    {
        Task<T> GetValueAsync<T>(string key);
        Task GetSetAsync<T>(string key, T value, uint hoursToLive);
    }
}
