namespace KSFramework.Primitives;

public abstract class EntityWithSoftDeleteRemoved : Entity
{
    protected EntityWithSoftDeleteRemoved(Guid id)
        : base(id)
    {
    }
    public bool IsDeleted { get; private set; } = false;

    public void Delete() => IsDeleted = true;

    public void Recover() => IsDeleted = false;
}