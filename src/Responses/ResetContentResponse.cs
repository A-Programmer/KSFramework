namespace KSFramework.Responses;

public class ResetContentResponse<T> : SuccessResponse<T>
{
    public ResetContentResponse(T? result)
        : base(205, result)
    {
        
    }
}

