namespace KSFramework.Exceptions;

public class KSBadRequestException<T> : KSException<T>
{
    public KSBadRequestException()
        : base(code: 400)
    {
        
    }
    
    public KSBadRequestException(string message)
        : base(code: 400, message: message)
    {
        
    }
    
    public KSBadRequestException(string message, T? errors)
        : base(code: 400, errors: errors, message: message)
    {
        
    }
}
public class KSBadRequestException : KSException
{
    public KSBadRequestException()
        : base(code: 400)
    {
        
    }
    
    public KSBadRequestException(string message)
        : base(code: 400, message)
    {
        
    }
}