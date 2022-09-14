namespace KSFramework.Responses;

public class NoContentResponse<T> : SuccessResponse<T>
{
    public NoContentResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}

