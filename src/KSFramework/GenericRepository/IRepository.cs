using System.Linq.Expressions;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Pagination;

namespace KSFramework.GenericRepository;
public interface IRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> GetByIdAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false);
    PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate);

}