using System.Linq.Expressions;
using KSFramework.Pagination;
using KSFramework.Primitives;

namespace KSFramework.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : AggregateRoot
{
    ValueTask<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, string orderBy = "", bool desc = false, CancellationToken cancellationToken = default(CancellationToken));
    PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, string orderBy = "", bool desc = false);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<bool> IsExistValuForPropertyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

}