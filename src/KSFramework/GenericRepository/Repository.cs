using System.Linq.Expressions;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Pagination;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.GenericRepository;
/// <summary>
/// Provides a base implementation of the generic repository pattern for entity operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext Context;
    protected DbSet<TEntity> Entity;

    /// <summary>
    /// Initializes a new instance of the Repository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    protected Repository(DbContext context)
    {
        this.Context = context;
        Entity = Context.Set<TEntity>();
    }

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task AddAsync(TEntity entity)
    {
        await Entity.AddAsync(entity);
    }

    /// <summary>
    /// Adds multiple entities to the repository asynchronously.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await Entity.AddRangeAsync(entities);
    }

    /// <summary>
    /// Finds entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter entities.</param>
    /// <returns>A collection of entities that match the condition.</returns>
    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Entity.Where(predicate);
    }

    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, containing all entities.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Entity.ToListAsync();
    }

    /// <summary>
    /// Retrieves a paginated list of entities asynchronously.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="where">Optional predicate to filter the entities.</param>
    /// <param name="orderBy">Optional property name to order the results by.</param>
    /// <param name="desc">Optional flag to indicate descending order.</param>
    /// <returns>A task representing the asynchronous operation, containing a paginated list of entities.</returns>
    public virtual async Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false)
    {
        var query = Entity.AsQueryable();
        if (where != null)
            query = query.Where(where);

        return await PaginatedList<TEntity>.CreateAsync(query, pageIndex, pageSize, where, orderBy, desc);
    }

    /// <summary>
    /// Retrieves a paginated list of entities synchronously.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="where">Optional predicate to filter the entities.</param>
    /// <param name="orderBy">Optional property name to order the results by.</param>
    /// <param name="desc">Optional flag to indicate descending order.</param>
    /// <returns>A paginated list of entities.</returns>
    public virtual PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false)
    {
        var query = Entity.AsQueryable();
        return PaginatedList<TEntity>.Create(query, pageIndex, pageSize, where, orderBy, desc);
    }

    /// <summary>
    /// Retrieves an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task representing the asynchronous operation, containing the entity if found.</returns>
    public virtual async ValueTask<TEntity?> GetByIdAsync(object id)
    {
        return await Entity.FindAsync(id);
    }

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public virtual void Remove(TEntity entity)
    {
        Entity.Remove(entity);
    }

    /// <summary>
    /// Removes multiple entities from the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to remove.</param>
    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    /// <summary>
    /// Retrieves a single entity that matches the specified predicate, or null if no match is found.
    /// </summary>
    /// <param name="predicate">The condition to filter the entity.</param>
    /// <returns>A task representing the asynchronous operation, containing the entity if found.</returns>
    public virtual async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Entity.SingleOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Checks if any entity exists that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to check for existence.</param>
    /// <returns>A task representing the asynchronous operation, containing true if a matching entity exists.</returns>
    public virtual async Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Entity.AnyAsync(predicate);
    }
}