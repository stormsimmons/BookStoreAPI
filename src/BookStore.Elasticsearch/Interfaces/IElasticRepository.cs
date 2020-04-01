using BookStore.Domain.CommandCriteria;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Domain.QueryCriteria;
using BookStore.Elasticsearch.CommandObjects;
using BookStore.Elasticsearch.QueryObjects;
using System.Threading.Tasks;

namespace BookStore.Elasticsearch.Interfaces
{
    public interface IElasticRepository
    {
        Task<TResponse> SearchAsync<T, TResponse, TCriteria>(ElasticQueryObject<T, TResponse, TCriteria> queryObject, TCriteria queryCriteria)
            where T : class, IEntity
            where TCriteria : QueryCriteria;
        Task<OperationResult> PutOneAsync<T, TCriteria>(ElasticCommandObject<T, TCriteria> commandObject, TCriteria commandCriteia)
            where T : class, IEntity
            where TCriteria : CommandCriteria;
    }
}
