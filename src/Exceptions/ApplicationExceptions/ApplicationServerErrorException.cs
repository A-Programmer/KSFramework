namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationServerErrorException<T> : ApplicationException<T>
{
    public ApplicationServerErrorException()
        : base(code: 500)
    {
        
    }
    public ApplicationServerErrorException(string message)
        : base(code: 500, message)
    {
        
    }
    public ApplicationServerErrorException(string message, T? errors)
        : base(code: 500, errors: errors, message)
    {
        
    }
}
public class ApplicationServerErrorException : ApplicationException
{
    public ApplicationServerErrorException()
        : base(code: 500)
    {
        
    }
    public ApplicationServerErrorException(string message)
        : base(code: 500, message)
    {
        
    }
}