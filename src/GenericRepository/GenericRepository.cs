using System.Linq.Expressions;
using KSFramework.Pagination;
using KSFramework.Primitives;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.GenericRepository;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AggregateRoot
{
    protected readonly DbContext Context;
    protected DbSet<TEntity> Entity;
    public GenericRepository(DbContext context)
    {
        this.Context = context;
        Entity = Context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Entity.AddRangeAsync(entities, cancellationToken);
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Entity.Where(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Entity.ToListAsync(cancellationToken);
    }

    public virtual async Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, string orderBy = "", bool desc = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await PaginatedList<TEntity>.CreateAsync(Entity.AsQueryable(), pageIndex, pageSize, where, orderBy, desc, cancellationToken);
    }

    public virtual PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where = null, string orderBy = "", bool desc = false)
    {
        return PaginatedList<TEntity>.Create(Entity.AsQueryable(), pageIndex, pageSize, where, orderBy, desc);
    }

    public virtual async ValueTask<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Entity.FindAsync(new[] { id }, cancellationToken: cancellationToken);
    }

    public virtual void Remove(TEntity entity)
    {
        Entity.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Entity.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<bool> IsExistValuForPropertyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Entity.AnyAsync(predicate, cancellationToken);
    }
}