namespace KSFramework.Responses;

public class ResetContentResponse<T> : SuccessResponse<T>
{
    public ResetContentResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}

