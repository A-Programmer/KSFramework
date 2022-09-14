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
}