using BookStore.Domain.Models;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<TResponse> Query<TEntity, TResponse, TCriteria>(TCriteria queryCriteria)
            where TEntity : class, IEntity
            where TCriteria : QueryCriteria.QueryCriteria;
        Task<OperationResult> Upsert<TEntity, TCriteria>(TCriteria commandCriteria)
            where TEntity : class, IEntity
            where TCriteria : CommandCriteria.CommandCriteria;
    }
}
