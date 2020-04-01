using BookStore.Domain.Enums;

namespace BookStore.Elasticsearch.Interfaces
{
    public interface IElasticQuery
    {
        QueryType QueryType { get; }
    }
}
