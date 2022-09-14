namespace KSFramework.Exceptions.ApplicationExceptions;

public abstract class ApplicationException<T> : Exception
{
    public int Code { get; protected set; }
    public T? Errors { get; set; }

    public ApplicationException(int code)
    {
        Code = code;
    }

    public ApplicationException(string message)
        : base(message)
    {
    }

    public ApplicationException(int code, string message)
        : base(message)
    {
        Code = code;
    }

    public ApplicationException(int code, T? errors, string message)
        : base(message)
    {
        Code = code;
        Errors = errors;
    }
}
public abstract class ApplicationException : Exception
{
    public int Code { get; protected set; }

    public ApplicationException(int code)
    {
        Code = code;
    }

    public ApplicationException(string message)
        : base(message)
    {
    }

    public ApplicationException(int code, string message)
        : base(message)
    {
        Code = code;
    }
}