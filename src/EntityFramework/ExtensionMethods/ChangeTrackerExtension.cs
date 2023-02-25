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
                EntityWithSoftDelete entity = (EntityWithSoftDelete)entry.Entity;
                entity.Delete();
                entry.State = EntityState.Modified;
            }
        }

        var aggregateRootsArray = aggregateRoots as EntityEntry[] ?? aggregateRoots.ToArray();
        if (aggregateRootsArray.Any())
        {
            foreach(EntityEntry entry in aggregateRootsArray)
            {
                AggregateRootWithSoftDelete entity = (AggregateRootWithSoftDelete)entry.Entity;
                entity.Delete();
                entry.State = EntityState.Modified;
            }
        }
    }
}