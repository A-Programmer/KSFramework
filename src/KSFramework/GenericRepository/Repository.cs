using System.Linq.Expressions;
using KSFramework.Domain.AggregatesHelper;
using KSFramework.Pagination;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.GenericRepository;
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    private readonly DbContext Context;
    protected DbSet<TEntity> Entity;
    public Repository(DbContext context)
    {
        this.Context = context;
        Entity = Context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Entity.AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await Entity.AddRangeAsync(entities);
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Entity.Where(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Entity.ToListAsync();
    }

    public async Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, string orderBy = "", bool desc = false)
    {
        return await PaginatedList<TEntity>.CreateAsync(Entity.AsQueryable(), pageIndex, pageSize, where, orderBy, desc);
    }

    public PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, string orderBy = "", bool desc = false)
    {
        return PaginatedList<TEntity>.Create(Entity.AsQueryable(), pageIndex, pageSize, where, orderBy, desc);
    }

    public virtual async ValueTask<TEntity> GetByIdAsync(object id)
    {
        return await Entity.FindAsync(id);
    }

    public void Remove(TEntity entity)
    {
        Entity.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Entity.SingleOrDefaultAsync(predicate);
    }

    public async Task<bool> IsExistValuForPropertyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Entity.AnyAsync(predicate);
    }
}