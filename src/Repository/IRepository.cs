using System;
using System.Linq.Expressions;
using KSFramework.Domain.AggregatesHelper;

namespace KSFramework.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        ValueTask<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<bool> IsExistValuForPropertyAsync(Expression<Func<TEntity, bool>> predicate);

    }
}

