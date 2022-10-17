namespace KSFramework.Exceptions;

public class KSUnauthorizedAccessException<T> : KSException<T>
{
    public KSUnauthorizedAccessException()
        : base(code: 401)
    {
        
    }
    public KSUnauthorizedAccessException(string message)
        : base(code: 401, message)
    {
        
    }
    public KSUnauthorizedAccessException(string message, T? errors)
        : base(code: 401, errors: errors, message)
    {
        
    }
}
public class KSUnauthorizedAccessException : KSException
{
    public KSUnauthorizedAccessException()
        : base(code: 401)
    {
        
    }
    public KSUnauthorizedAccessException(string message)
        : base(code: 401, message)
    {
        
    }
}