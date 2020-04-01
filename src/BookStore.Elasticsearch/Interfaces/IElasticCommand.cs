
using BookStore.Domain.Enums;

namespace BookStore.Elasticsearch.Interfaces
{
    public interface IElasticCommand
    {
        CommandType CommandType { get; }
    }
}
