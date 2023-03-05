namespace KSFramework.Primitives;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset DeletedAt { get; set; }
    public void Delete();
    public void Recover();
}