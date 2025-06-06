using System.Linq.Expressions;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Pagination;

namespace KSFramework.GenericRepository;

/// <summary>
/// Represents a generic repository interface for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found.</returns>
    ValueTask<TEntity?> GetByIdAsync(object id);

    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves a paginated list of entities asynchronously based on specified criteria.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve (1-based).</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="where">Optional predicate to filter the entities.</param>
    /// <param name="orderBy">Optional property name to order the results by.</param>
    /// <param name="desc">Optional flag to indicate descending order.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paginated list of entities.</returns>
    Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false);

    /// <summary>
    /// Retrieves a paginated list of entities synchronously based on specified criteria.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve (1-based).</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="where">Optional predicate to filter the entities.</param>
    /// <param name="orderBy">Optional property name to order the results by.</param>
    /// <param name="desc">Optional flag to indicate descending order.</param>
    /// <returns>A paginated list of entities.</returns>
    PaginatedList<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? where = null, string? orderBy = "", bool desc = false);

    /// <summary>
    /// Finds entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter entities.</param>
    /// <returns>A collection of entities that match the condition.</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity that matches the specified predicate, or null if no match is found.
    /// </summary>
    /// <param name="predicate">The condition to filter the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, otherwise null.</returns>
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Adds multiple entities to the repository asynchronously.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// Removes multiple entities from the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to remove.</param>
    void RemoveRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Checks if any entity exists that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to check for existence.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if a matching entity exists; otherwise, false.</returns>
    Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate);
}
