using KSFramework.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSFramework.EntityFramework.Maps.Base;

public abstract class SoftDeleteMapConfiguration<TWithSoftDelete>
    : ISoftDeleteMapConfiguration<TWithSoftDelete> where TWithSoftDelete : class, ISoftDelete
{
    public virtual void Configure(EntityTypeBuilder<TWithSoftDelete> builder)
    {
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}