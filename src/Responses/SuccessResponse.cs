namespace KSFramework.Responses;

public abstract class SuccessResponse<T>
{
    public SuccessResponse(int code)
    {
        Code = code;
    }
    public SuccessResponse(int code, T? result)
    {
        Code = code;
        Result = result;
    }

    public int Code { get; protected set; } = 200;
    public T? Result { get; set; }
}

