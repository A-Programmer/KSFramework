using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KSFramework.GenericRepository;

/// <summary>
/// Base class for EF Core repositories with reusable query helpers.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class Repository<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets the EF DbContext instance.
    /// </summary>
    protected readonly DbContext Context;

    /// <summary>
    /// Gets the EF DbSet for the entity.
    /// </summary>
    protected readonly DbSet<TEntity> DbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The EF DbContext.</param>
    protected Repository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Returns an IQueryable for the entity, optionally with AsNoTracking.
    /// </summary>
    /// <param name="asNoTracking">Whether to disable tracking.</param>
    /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
    protected IQueryable<TEntity> AsQueryable(bool asNoTracking = true)
    {
        return asNoTracking ? DbSet.AsNoTracking() : DbSet.AsQueryable();
    }

    /// <summary>
    /// Applies a where filter to a query.
    /// </summary>
    /// <param name="query">The base query.</param>
    /// <param name="where">The where expression.</param>
    /// <returns>The filtered query.</returns>
    protected IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? where)
    {
        return where != null ? query.Where(where) : query;
    }
}