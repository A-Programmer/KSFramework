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

        if (entities.Any())
        {
            foreach(EntityEntry entry in entities)
            {
                EntityWithSoftDelete entity = (EntityWithSoftDelete)entry.Entity;
                entity.Delete();
                entry.State = EntityState.Modified;
            }
        }
    }
}