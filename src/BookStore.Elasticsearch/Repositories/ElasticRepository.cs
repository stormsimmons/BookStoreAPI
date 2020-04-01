using BookStore.Domain.CommandCriteria;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Domain.QueryCriteria;
using BookStore.Elasticsearch.CommandObjects;
using BookStore.Elasticsearch.Interfaces;
using BookStore.Elasticsearch.QueryObjects;
using Nest;
using System.Threading.Tasks;

namespace BookStore.Elasticsearch.Repositories
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient _elasticClient;

        public ElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<TResponse> SearchAsync<T, TResponse, TCriteria>(ElasticQueryObject<T, TResponse, TCriteria> queryObject, TCriteria queryCriteria)
            where T : class, IEntity
            where TCriteria : QueryCriteria
        {
            var result = await _elasticClient.SearchAsync<T>(queryObject.BuildDescriptor(queryCriteria));
            return queryObject.FormatSearchResponse(result);
        }

        public async Task<OperationResult> PutOneAsync<T, TCriteria>(ElasticCommandObject<T, TCriteria> commandObject, TCriteria commandCriteia)
            where T : class, IEntity
            where TCriteria : CommandCriteria
        {
            var entityValue = commandObject.GetCommandValue(commandCriteia);

            var indexRespone = await _elasticClient.IndexAsync(entityValue, doc
                => doc.Id(entityValue.Id));

            return new OperationResult
            {
                IsSuccessful = indexRespone?.ApiCall?.Success ?? false
            };
        }


    }
}
