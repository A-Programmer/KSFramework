using System;
using System.Threading.Tasks;

namespace KSFramework.GenericRepository;

/// <summary>
/// Represents the Unit of Work pattern, encapsulating the transaction and saving changes.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Saves all changes made in this unit of work to the underlying data store asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    /// <inheritdoc cref="SaveChangesAsync(System.Threading.CancellationToken)"/>
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default);
    
    /// <inheritdoc cref="SaveChanges"/>
    int SaveChanges(bool acceptAllChangesOnSuccess);
    
    /// <inheritdoc cref="SaveChanges"/>
    int SaveChanges();

    /// <summary>
    /// Gets a generic repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of <see cref="IGenericRepository{TEntity}"/> for the specified entity type.</returns>
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}