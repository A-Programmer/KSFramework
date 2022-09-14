namespace KSFramework.Responses;

public class OkResponse<T> : SuccessResponse<T>
{
    public OkResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}

