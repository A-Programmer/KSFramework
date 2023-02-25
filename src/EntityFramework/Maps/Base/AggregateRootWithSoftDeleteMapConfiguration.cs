using KSFramework.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSFramework.EntityFramework.Maps.Base;

public abstract class AggregateRootWithSoftDeleteMapConfiguration<TAggregateRootWithSoftDelete>
    : IAggregateRootWithSoftDeleteMapConfiguration<TAggregateRootWithSoftDelete> where TAggregateRootWithSoftDelete : AggregateRootWithSoftDelete
{
    public virtual void Configure(EntityTypeBuilder<TAggregateRootWithSoftDelete> builder)
    {
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}