namespace KSFramework.Responses;

public class PartialContentResponse<T> : SuccessResponse<T>
{
    public PartialContentResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}