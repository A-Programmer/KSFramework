using FluentValidation.Results;

namespace KSFramework.Exceptions.ApplicationExceptions;

public class ApplicationFormValidationException<T> : ApplicationException<T>
{
    public IEnumerable<ValidationFailure> Errors;
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
    public IEnumerable<ValidationFailure> Errors;
    public ApplicationFormValidationException()
        : base(code: 400)
    {
        
    }
    public ApplicationFormValidationException(string message)
        : base(code: 400, message)
    {
        
    }
}