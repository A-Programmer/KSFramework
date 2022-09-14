namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationUnauthorizedAccessException<T> : ApplicationException<T>
{
    public ApplicationUnauthorizedAccessException()
        : base(code: 401)
    {
        
    }
    public ApplicationUnauthorizedAccessException(string message)
        : base(code: 401, message)
    {
        
    }
    public ApplicationUnauthorizedAccessException(string message, T? errors)
        : base(code: 401, errors: errors, message)
    {
        
    }
}
public class ApplicationUnauthorizedAccessException : ApplicationException
{
    public ApplicationUnauthorizedAccessException()
        : base(code: 401)
    {
        
    }
    public ApplicationUnauthorizedAccessException(string message)
        : base(code: 401, message)
    {
        
    }
}