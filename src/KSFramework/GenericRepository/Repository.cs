using System.Linq.Expressions;
using KSFramework.Pagination;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.GenericRepository;

/// <summary>
/// Provides a base implementation of the <see cref="IGenericRepository{TEntity}"/> interface using Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public abstract class Repository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbContext Context;
    /// <summary>
    /// Gets the underlying <see cref="DbSet{TEntity}"/> for querying and persisting entities.
    /// </summary>
    protected readonly DbSet<TEntity> Entity;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    protected Repository(DbContext context)
    {
        Context = context;
        Entity = context.Set<TEntity>();
    }

    /// <inheritdoc />
    public virtual async Task AddAsync(TEntity entity)
    {
        await Entity.AddAsync(entity);
    }

    /// <inheritdoc />
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await Entity.AddRangeAsync(entities);
    }

    /// <inheritdoc />
    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Entity.Where(predicate);
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Entity.ToListAsync();
    }

    /// <inheritdoc />
    public virtual async Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false)
    {
        var query = Entity.AsQueryable();
        if (where != null)
            query = query.Where(where);

        return await PaginatedList<TEntity>.CreateAsync(query, pageIndex, pageSize, where, orderBy, desc);
    }

    /// <inheritdoc />
    public virtual PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false)
    {
        var query = Entity.AsQueryable();
        return PaginatedList<TEntity>.Create(query, pageIndex, pageSize, where, orderBy, desc);
    }

    /// <inheritdoc />
    public virtual async ValueTask<TEntity?> GetByIdAsync(object id)
    {
        return await Entity.FindAsync(id);
    }

    /// <inheritdoc />
    public virtual void Remove(TEntity entity)
    {
        Entity.Remove(entity);
    }

    /// <inheritdoc />
    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Entity.SingleOrDefaultAsync(predicate);
    }

    /// <inheritdoc />
    public virtual async Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Entity.AnyAsync(predicate);
    }
}