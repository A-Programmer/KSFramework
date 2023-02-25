using Microsoft.EntityFrameworkCore;

namespace KSFramework.EntityFramework.Maps.Base;

public interface IEntityWithSoftDeleteMapConfiguration<TEntityWithSoftDelete>
    : IEntityTypeConfiguration<TEntityWithSoftDelete> where TEntityWithSoftDelete : class
{

}