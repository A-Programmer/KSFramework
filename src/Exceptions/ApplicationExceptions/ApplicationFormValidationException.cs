namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationFormValidationException<T> : ApplicationException<T>
{
    public ApplicationFormValidationException()
        : base(code: 400)
    {
        
    }
    public ApplicationFormValidationException(string message)
        : base(code: 400, message)
    {
        
    }
}