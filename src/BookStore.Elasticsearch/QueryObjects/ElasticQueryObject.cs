using BookStore.Domain.Enums;
using BookStore.Domain.Interfaces;
using BookStore.Domain.QueryCriteria;
using BookStore.Elasticsearch.Interfaces;
using Nest;
using System;

namespace BookStore.Elasticsearch.QueryObjects
{
    public abstract class ElasticQueryObject< T, TResponse, TCriteria> : IElasticQuery
        where T : class, IEntity
        where TCriteria : QueryCriteria
    {
        private QueryType? _queryType = null;
        public QueryType QueryType
        {
            get
            {
                if (_queryType == null)
                {
                    throw new NotImplementedException();
                }
                return (QueryType)_queryType;
            }
            protected set => _queryType = value;
        }

        public abstract SearchDescriptor<T> BuildDescriptor(TCriteria queryCriteria);
        public abstract TResponse FormatSearchResponse(ISearchResponse<T> searchResponse);
    }
}
