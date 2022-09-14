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
    public ApplicationFormValidationException(string message, T? errors)
        : base(code: 400, errors: errors, message)
    {
        
    }
}
public class ApplicationFormValidationException: ApplicationException
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