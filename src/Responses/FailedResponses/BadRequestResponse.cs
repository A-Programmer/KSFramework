namespace KSFramework.Responses.FailedResponses;

public class BadRequestResponse<T> : FailedResponse<T>
{
    public BadRequestResponse(string message, T? result)
        : base(400, message, result)
    {
        
    }
    public BadRequestResponse(T? result)
        : base(400, result)
    {
        
    }
}

public class BadRequestResponse : FailedResponse
{
    public BadRequestResponse()
        : base(400)
    {
        
    }
}