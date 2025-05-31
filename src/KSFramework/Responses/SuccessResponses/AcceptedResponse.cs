namespace KSFramework.Responses.SuccessResponses;

public class AcceptedResponse<T> : BaseResponse<T>
{
    public AcceptedResponse(T? result)
        : base(202, result)
    {
        
    }
}


public class AcceptedResponse : BaseResponse
{
    public AcceptedResponse()
        : base(202)
    {
        
    }
}

