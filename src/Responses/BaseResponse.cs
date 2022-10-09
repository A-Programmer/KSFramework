namespace KSFramework.Responses;

public abstract class BaseResponse<T>
{
    public BaseResponse(int code)
    {
        Code = code;
    }
    public BaseResponse(int code, T? result)
    {
        Code = code;
        Result = result;
    }

    public int Code { get; protected set; } = 200;
    public T? Result { get; set; }
}

public abstract class BaseResponse
{
    public BaseResponse(int code)
    {
        Code = code;
    }

    public int Code { get; protected set; } = 200;
}

