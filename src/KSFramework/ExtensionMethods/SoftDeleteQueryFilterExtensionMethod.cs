using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.ExtensionMethods;

public static class SoftDeleteQueryFilterExtensionMethod
{
    public static void ApplyGlobalSoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entitytype in modelBuilder.Model.GetEntityTypes())
        {
            var types = entitytype.ClrType;
            var isDeletedProperty = types.GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, "IsDeleted", StringComparison.OrdinalIgnoreCase));
            if (isDeletedProperty != null)
            {
                var parameter = Expression.Parameter(types);
                var property = Expression.Property(parameter, isDeletedProperty);
                var noDeleted = Expression.Not(property);
                var lambda = Expression.Lambda(noDeleted, parameter);
                modelBuilder.Entity(types).HasQueryFilter(lambda);
            }
        }
    }
}