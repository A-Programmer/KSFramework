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
                .Where(t => t.Entity is ISoftDelete && t.State == EntityState.Deleted);

        var entityEntriesArray = entities as EntityEntry[] ?? entities.ToArray();
        if (entityEntriesArray.Any())
        {
            foreach(EntityEntry entry in entityEntriesArray)
            {
                ISoftDelete entity = (ISoftDelete)entry.Entity;

                entry.State = EntityState.Unchanged;
                entity.Delete();

                changeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
                changeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
            }
        }
    }
}