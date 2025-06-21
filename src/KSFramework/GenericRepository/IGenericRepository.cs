using System.Linq.Expressions;
using KSFramework.Pagination;

namespace KSFramework.GenericRepository;

/// <summary>
/// Represents a generic repository contract for performing common data access operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Asynchronously retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task representing the asynchronous operation, containing the entity if found; otherwise, null.</returns>
    ValueTask<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves all entities.
    /// </summary>
    /// <param name="asNoTracking">Whether to track entities in change tracker.</param>
    /// <returns>A task containing all entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a paginated list of entities with optional filtering and ordering.
    /// </summary>
    /// <param name="pageIndex">The index of the page (starting from 1).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="where">Optional filter expression.</param>
    /// <param name="orderBy">Optional property name to order by.</param>
    /// <param name="desc">Indicates if the order should be descending.</param>
    /// <returns>A task containing a paginated list of entities.</returns>
    Task<PaginatedList<TEntity>> GetPagedAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        string? orderBy = "",
        bool desc = false);

    /// <summary>
    /// Retrieves a paginated list of entities with optional filtering and ordering.
    /// </summary>
    /// <param name="pageIndex">The index of the page (starting from 1).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="where">Optional filter expression.</param>
    /// <param name="orderBy">Optional property name to order by.</param>
    /// <param name="desc">Indicates if the order should be descending.</param>
    /// <returns>A paginated list of entities.</returns>
    PaginatedList<TEntity> GetPaged(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        string? orderBy = "",
        bool desc = false);

    /// <summary>
    /// Finds entities matching the given predicate.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <param name="asNoTracking">Whether to track entities in change tracker.</param>
    /// <returns>A collection of matching entities.</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);

    /// <summary>
    /// Asynchronously returns a single entity matching the given predicate, or null if none match.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <returns>A task containing a single entity or null.</returns>
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds an entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken"></param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds multiple entities to the repository.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken"></param>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// Removes multiple entities from the repository.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    void RemoveRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Asynchronously checks whether any entity matches the given predicate.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if any entity matches; otherwise, false.</returns>
    Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}