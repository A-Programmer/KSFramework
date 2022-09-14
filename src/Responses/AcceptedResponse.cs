namespace KSFramework.Responses;

public class AcceptedResponse<T> : SuccessResponse<T>
{
    public AcceptedResponse(T? result)
        : base(202, result)
    {
        
    }
}


public class AcceptedResponse : SuccessResponse
{
    public AcceptedResponse()
        : base(202)
    {
        
    }
}

