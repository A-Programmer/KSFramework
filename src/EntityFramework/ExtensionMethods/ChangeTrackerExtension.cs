using KSFramework.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KSFramework.EntityFramework.ExtensionMethods;

public static class ChangeTrackerExtensions
{
    public static void SetAuditProperties(this ChangeTracker changeTracker)
    {
        changeTracker.DetectChanges();
        
        IEnumerable<EntityEntry> entities =
            changeTracker
                .Entries()
                .Where(t => t.Entity is EntityWithSoftDelete && t.State == EntityState.Deleted);
        
        IEnumerable<EntityEntry> aggregateRoots =
            changeTracker
                .Entries()
                .Where(t => t.Entity is AggregateRootWithSoftDelete && t.State == EntityState.Deleted);

        var entityEntriesArray = entities as EntityEntry[] ?? entities.ToArray();
        if (entityEntriesArray.Any())
        {
            foreach(EntityEntry entry in entityEntriesArray)
            {
                entry.State = EntityState.Modified;
                entry.Member("IsDeleted").CurrentValue = true;
            }
        }

        var aggregateRootsArray = aggregateRoots as EntityEntry[] ?? aggregateRoots.ToArray();
        if (aggregateRootsArray.Any())
        {
            foreach(EntityEntry entry in aggregateRootsArray)
            {
                entry.State = EntityState.Modified;
                entry.Member("IsDeleted").CurrentValue = true;
            }
        }
    }
}