namespace KSFramework.Exceptions;

public class KSException<T> : Exception
{
    public int Code { get; protected set; }
    public T? Errors { get; set; }

    public KSException(int code)
    {
        Code = code;
    }

    public KSException(string message)
        : base(message)
    {
    }

    public KSException(int code, string message)
        : base(message)
    {
        Code = code;
    }

    public KSException(int code, T? errors, string message)
        : base(message)
    {
        Code = code;
        Errors = errors;
    }
}
public class KSException : Exception
{
    public int Code { get; protected set; }

    public KSException(int code)
    {
        Code = code;
    }

    public KSException(string message)
        : base(message)
    {
    }

    public KSException(int code, string message)
        : base(message)
    {
        Code = code;
    }
}