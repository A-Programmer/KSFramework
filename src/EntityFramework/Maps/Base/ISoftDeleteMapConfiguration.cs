using Microsoft.EntityFrameworkCore;

namespace KSFramework.EntityFramework.Maps.Base;

public interface ISoftDeleteMapConfiguration<TWithSoftDelete>
    : IEntityTypeConfiguration<TWithSoftDelete> where TWithSoftDelete : class
{

}