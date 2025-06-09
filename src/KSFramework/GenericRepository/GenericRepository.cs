using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using KSFramework.Pagination;

namespace KSFramework.GenericRepository;

/// <summary>
/// Final generic repository class that implements IGenericRepository using EF Core.
/// Provides basic CRUD and pagination operations.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public class GenericRepository<TEntity> : Repository<TEntity>, IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GenericRepository(DbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets an entity by its primary key.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public async ValueTask<TEntity?> GetByIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <param name="asNoTracking">Whether to disable tracking for better performance.</param>
    /// <returns>A list of all entities.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = true)
    {
        return await AsQueryable(asNoTracking).ToListAsync();
    }

    /// <summary>
    /// Gets a paginated list of entities asynchronously.
    /// </summary>
    /// <param name="pageIndex">Page index (starting from 1).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="where">Optional filter expression.</param>
    /// <param name="orderBy">Property name to order by.</param>
    /// <param name="desc">Order descending if true.</param>
    /// <returns>A paginated list of entities.</returns>
    public async Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false)
    {
        var query = ApplyWhere(AsQueryable(), where);
        return await PaginatedList<TEntity>.CreateAsync(query, pageIndex, pageSize, where, orderBy, desc);
    }

    /// <summary>
    /// Gets a paginated list of entities synchronously.
    /// </summary>
    /// <param name="pageIndex">Page index (starting from 1).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="where">Optional filter expression.</param>
    /// <param name="orderBy">Property name to order by.</param>
    /// <param name="desc">Order descending if true.</param>
    /// <returns>A paginated list of entities.</returns>
    public PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false)
    {
        var query = ApplyWhere(AsQueryable(), where);
        return PaginatedList<TEntity>.Create(query, pageIndex, pageSize, where, orderBy, desc);
    }

    /// <summary>
    /// Finds entities based on the given predicate.
    /// </summary>
    /// <param name="predicate">The filter expression.</param>
    /// <param name="asNoTracking">Whether to disable tracking.</param>
    /// <returns>List of matching entities.</returns>
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
    {
        return AsQueryable(asNoTracking).Where(predicate).ToList();
    }

    /// <summary>
    /// Gets a single entity that matches the given predicate or null.
    /// </summary>
    /// <param name="predicate">The filter expression.</param>
    /// <returns>The matching entity or null.</returns>
    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.SingleOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    /// <summary>
    /// Adds a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    /// <summary>
    /// Removes a range of entities.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    /// <summary>
    /// Determines whether any entity exists that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to check.</param>
    /// <returns>True if at least one entity exists; otherwise, false.</returns>
    public async Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AnyAsync(predicate);
    }
}