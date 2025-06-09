
namespace KSFramework.KSDomain;

/// <summary>
/// Base class for entities with GUID as the primary key that can be used with the generic repository.
/// </summary>
public abstract class Entity : BaseEntity
{
    protected Entity(Guid id) : base(id)
    {
    }
    protected Entity() {}
}