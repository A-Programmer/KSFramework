using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KSFramework.GenericRepository;

/// <summary>
/// Implements the Unit of Work pattern, managing a DbContext and providing access to generic repositories.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;
    /// <summary>
    /// Initializes a new instance of the UnitOfWork class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UnitOfWork(DbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return _context.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// Saves all changes made in this unit of work to the underlying data store asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    /// <summary>
    /// Saves all changes made in this unit of work to the underlying data store asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a generic repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of <see cref="IGenericRepository{TEntity}"/> for the specified entity type.</returns>
    IGenericRepository<TEntity> IUnitOfWork.GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.TryGetValue(typeof(TEntity), out var repository))
        {
            return (IGenericRepository<TEntity>)repository;
        }

        var newRepository = new GenericRepository<TEntity>((DbContext)_context);
        _repositories.Add(typeof(TEntity), newRepository);
        return newRepository;
    }

    private bool _disposed = false;

    /// <summary>
    /// Disposes the current instance of the UnitOfWork.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the current instance of the UnitOfWork.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
}