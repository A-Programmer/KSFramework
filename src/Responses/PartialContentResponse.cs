namespace KSFramework.Responses;

public class PartialContentResponse<T> : SuccessResponse<T>
{
    public PartialContentResponse(T? result)
        : base(206, result)
    {
        
    }
}

public class PartialContentResponse : SuccessResponse
{
    public PartialContentResponse()
        : base(206)
    {
        
    }
}