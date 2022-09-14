namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationBadRequestException<T> : ApplicationException<T>
{
    public ApplicationBadRequestException()
        : base(code: 400)
    {
        
    }
}