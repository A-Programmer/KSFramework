using FluentValidation.Results;

namespace KSFramework.Exceptions;

public class KSValidationException<T> : KSException<T>
{
    private IEnumerable<ValidationFailure>? _validationErrors;
    public new IEnumerable<ValidationFailure>? Errors
    {
        get => _validationErrors;
        set => _validationErrors = value;
    }
    public KSValidationException()
        : base(code: 400)
    {

    }
    public KSValidationException(string message)
        : base(code: 400, message)
    {

    }
    public KSValidationException(string message, T? errors)
        : base(code: 400, errors: errors, message)
    {

    }
}
public class KSValidationException : KSException<IEnumerable<ValidationFailure>>
{
    public KSValidationException()
        : base(code: 400)
    {

    }
    public KSValidationException(string message)
        : base(code: 400, message)
    {

    }
}