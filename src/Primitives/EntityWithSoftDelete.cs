namespace KSFramework.Primitives;

public class EntityWithSoftDelete : Entity
{
    protected EntityWithSoftDelete(Guid id)
        : base(id)
    {
    }
    public bool IsDeleted { get; private set; } = false;

    public void Delete() => IsDeleted = true;

    public void Recover() => IsDeleted = false;
}