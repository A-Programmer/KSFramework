namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationBadRequestException<T> : ApplicationException<T>
{
    public ApplicationBadRequestException()
        : base(code: 400)
    {
        
    }
    
    public ApplicationBadRequestException(string message)
        : base(code: 400, message: message)
    {
        
    }
    
    public ApplicationBadRequestException(string message, T? errors)
        : base(code: 400, errors: errors, message: message)
    {
        
    }
}
public class ApplicationBadRequestException : ApplicationException
{
    public ApplicationBadRequestException()
        : base(code: 400)
    {
        
    }
    
    public ApplicationBadRequestException(string message)
        : base(code: 400, message)
    {
        
    }
}