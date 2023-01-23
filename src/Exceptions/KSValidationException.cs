using FluentValidation.Results;

namespace KSFramework.Exceptions;

public class KSValidationException<T> : KSException<T>
{
    public IEnumerable<ValidationFailure> Errors;
    public KSValidationException()
        : base(code: 400)
    {
        
    }
    public KSValidationException(string message)
        : base(code: 400, message)
    {
        
    }
    public KSValidationException(IEnumerable<ValidationFailure> errors)
        : base(code: 400)
    {
        Errors = errors;
    }
    public KSValidationException(string message, T? errors)
        : base(code: 400, errors: errors, message)
    {
        
    }
}
public class KSValidationException: KSException
{
    public IEnumerable<ValidationFailure> Errors;
    public KSValidationException()
        : base(code: 400)
    {
        
    }
    public KSValidationException(IEnumerable<ValidationFailure> errors)
        : base(code: 400)
    {
        Errors = errors;
    }
    public KSValidationException(string message)
        : base(code: 400, message)
    {
        
    }
}