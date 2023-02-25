using Microsoft.EntityFrameworkCore;

namespace KSFramework.EntityFramework.Maps.Base;

public interface IAggregateRootWithSoftDeleteMapConfiguration<TAggregateRootWithSoftDelete>
    : IEntityTypeConfiguration<TAggregateRootWithSoftDelete> where TAggregateRootWithSoftDelete : class
{

}