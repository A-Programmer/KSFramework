using System.Linq.Expressions;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Pagination;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.GenericRepository;

/// <summary>
/// Generic repository implementation for entity operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public class GenericRepository<TEntity> : Repository<TEntity>, IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the GenericRepository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GenericRepository(DbContext context) : base(context)
    {
    }
}