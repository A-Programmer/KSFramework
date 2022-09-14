namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationServerErrorException<T> : ApplicationException<T>
{
    public ApplicationServerErrorException()
        : base(code: 500)
    {
        
    }
}