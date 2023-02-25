using KSFramework.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSFramework.EntityFramework.Maps.Base;

public abstract class EntityWithSoftDeleteMapConfiguration<TEntityWithSoftDelete>
    : IEntityWithSoftDeleteMapConfiguration<TEntityWithSoftDelete> where TEntityWithSoftDelete : EntityWithSoftDelete
{
    public virtual void Configure(EntityTypeBuilder<TEntityWithSoftDelete> builder)
    {
        builder.HasQueryFilter(t => t.IsDeleted == false);
    }
}