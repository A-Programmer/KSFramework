namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationFormValidationException<T> : ApplicationException<T>
{
    public ApplicationFormValidationException()
        : base(code: 400)
    {
        
    }
}