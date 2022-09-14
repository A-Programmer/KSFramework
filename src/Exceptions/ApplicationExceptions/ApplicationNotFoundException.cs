namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationNotFoundException<T> : ApplicationException<T>
{
    public ApplicationNotFoundException()
        : base(code: 404)
    {
        
    }
    public ApplicationNotFoundException(string message)
        : base(code: 404, message)
    {
        
    }
    public ApplicationNotFoundException(string message, T? errors)
        : base(code: 404, errors: errors, message)
    {
        
    }
}
public class ApplicationNotFoundException : ApplicationException
{
    public ApplicationNotFoundException()
        : base(code: 404)
    {
        
    }
    public ApplicationNotFoundException(string message)
        : base(code: 404, message)
    {
        
    }
}