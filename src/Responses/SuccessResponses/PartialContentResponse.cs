namespace KSFramework.Responses.SuccessResponses;

public class PartialContentResponse<T> : BaseResponse<T>
{
    public PartialContentResponse(T? result)
        : base(206, result)
    {
        
    }
}

public class PartialContentResponse : BaseResponse
{
    public PartialContentResponse()
        : base(206)
    {
        
    }
}