using System.Linq.Expressions;
using KSFramework.Pagination;

namespace KSFramework.GenericRepository;

/// <summary>
/// Defines the contract for a generic repository to handle basic CRUD and query operations.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = true);
    Task<PaginatedList<TEntity>> GetPagedAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        string? orderBy = "",
        bool desc = false);
    PaginatedList<TEntity> GetPaged(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        string? orderBy = "",
        bool desc = false);
    ValueTask<TEntity?> GetByIdAsync(object id);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate);
    void Update(TEntity entity);
}