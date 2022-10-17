namespace KSFramework.Exceptions;

public class KSNotFoundException<T> : KSException<T>
{
    public KSNotFoundException()
        : base(code: 404)
    {
        
    }
    public KSNotFoundException(string message)
        : base(code: 404, message)
    {
        
    }
    public KSNotFoundException(string message, T? errors)
        : base(code: 404, errors: errors, message)
    {
        
    }
}
public class KSNotFoundException : KSException
{
    public KSNotFoundException()
        : base(code: 404)
    {
        
    }
    public KSNotFoundException(string message)
        : base(code: 404, message)
    {
        
    }
}