using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Parking.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TId id, bool loadAllRelatedData = true);

        Task<IQueryable<TEntity>> GetAllAsync();

        Task <TEntity> GetSingleItemOnConditionAsync(Expression<Func<TEntity, bool>> singleClause, bool loadAllRelatedData);

        Task<IQueryable<TEntity>> GetAllItemsOfGivenEntityWithConditionAsync(Expression<Func<TEntity, bool>> whereClause, bool loadAllRelatedData = true);

        Task<IQueryable<TEntity>> GetAllItemsOfGivenEntityWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeExpressions);

        Task CreateAsync(TEntity entity);

        Task SaveAsync();

        Task UpdateAsync(TEntity entity);

    }
}
