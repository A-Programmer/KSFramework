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
}