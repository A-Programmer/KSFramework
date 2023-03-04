namespace KSFramework.Primitives;

public abstract class AggregateRootWithSoftDeleteRemoved : AggregateRoot
{
    protected AggregateRootWithSoftDeleteRemoved(Guid id) : base(id)
    {
    }
    public bool IsDeleted { get; set; } = false;

    public void Delete() => IsDeleted = true;

    public void Recover() => IsDeleted = false;
}