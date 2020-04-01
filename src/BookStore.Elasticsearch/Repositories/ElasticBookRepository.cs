using BookStore.Domain.CommandCriteria;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Domain.QueryCriteria;
using BookStore.Elasticsearch.CommandObjects;
using BookStore.Elasticsearch.Interfaces;
using BookStore.Elasticsearch.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Elasticsearch.Repositories
{
    public class ElasticBookRepository : IBookRepository
    {
        private readonly IElasticRepository _elasticRepository;
        private readonly IEnumerable<IElasticQuery> _queries;
        private readonly IEnumerable<IElasticCommand> _commands;

        public ElasticBookRepository(IElasticRepository elasticRepository, IEnumerable<IElasticQuery> queries, IEnumerable<IElasticCommand> commands)
        {
            _elasticRepository = elasticRepository;
            _queries = queries;
            _commands = commands;
        }

        public async Task<TResponse> Query<TEntity, TResponse, TCriteria>(TCriteria queryCriteria)
            where TEntity : class, IEntity
            where TCriteria : QueryCriteria
        {
            var query = _queries.FirstOrDefault(x => x.QueryType == queryCriteria.QueryType)
                as ElasticQueryObject<TEntity, TResponse, TCriteria>;

            if (query == null)
            {
                throw new NotImplementedException();
            }

            return await _elasticRepository.SearchAsync(query, queryCriteria);
        }

        public async Task<OperationResult> Upsert<TEntity, TCriteria>(TCriteria commandCriteria)
            where TEntity : class, IEntity
            where TCriteria : CommandCriteria
        {
            var command = _commands.FirstOrDefault(x => x.CommandType == commandCriteria.CommandType)
                as ElasticCommandObject<TEntity, TCriteria>;

            if (command == null)
            {
                throw new NotImplementedException();
            }

            return await _elasticRepository.PutOneAsync(command, commandCriteria);
        }
    }
}
