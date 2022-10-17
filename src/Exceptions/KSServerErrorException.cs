namespace KSFramework.Exceptions;

public class KSServerErrorException<T> : KSException<T>
{
    public KSServerErrorException()
        : base(code: 500)
    {
        
    }
    public KSServerErrorException(string message)
        : base(code: 500, message)
    {
        
    }
    public KSServerErrorException(string message, T? errors)
        : base(code: 500, errors: errors, message)
    {
        
    }
}
public class KSServerErrorException : KSException
{
    public KSServerErrorException()
        : base(code: 500)
    {
        
    }
    public KSServerErrorException(string message)
        : base(code: 500, message)
    {
        
    }
}