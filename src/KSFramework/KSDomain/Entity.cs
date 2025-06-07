using KSFramework.KSDomain.AggregatesHelper;

namespace KSFramework.KSDomain;

/// <summary>
/// Base class for entities with GUID as the primary key that can be used with the generic repository.
/// </summary>
public abstract class Entity : BaseEntity, IAggregateRoot
{
}