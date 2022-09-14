namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationNotFoundException<T> : ApplicationException<T>
{
    public ApplicationNotFoundException()
        : base(code: 404)
    {
        
    }
}