namespace KSFramework.Responses.SuccessResponses;

public class ResetContentResponse<T> : BaseResponse<T>
{
    public ResetContentResponse(T? result)
        : base(205, result)
    {
        
    }
}

public class ResetContentResponse : BaseResponse
{
    public ResetContentResponse()
        : base(205)
    {
        
    }
}

