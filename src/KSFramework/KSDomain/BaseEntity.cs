using System;
namespace KSFramework.KSDomain;
/// <summary>
/// Represents the base interface for all entities in the domain.
/// </summary>
public interface IEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}

/// <summary>
/// Base class for entities with GUID as the primary key.
/// </summary>
public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity() {}
    protected BaseEntity(Guid id)
    : base(id)
    {
    }
}

/// <summary>
/// Generic base class for entities with a specified key type.
/// </summary>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
public abstract class BaseEntity<TKey> : IEntity
{
    protected BaseEntity() {}
    protected BaseEntity(TKey id)
    {
        Id = id;
    }
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public TKey Id { get; set; }

    /// <summary>
    /// Gets the version number of the entity for optimistic concurrency.
    /// </summary>
    public int Version { get; private set; } = 0;

    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    /// <summary>
    /// Gets the collection of domain events associated with this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    protected void IncreaseVersion()
    {
        Version++;
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? ModifiedAt { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}