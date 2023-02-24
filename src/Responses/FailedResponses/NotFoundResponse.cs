namespace KSFramework.Responses.FailedResponses;

public class NotFoundResponse<T> : FailedResponse<T>
{
    public NotFoundResponse(string message, T? result)
        : base(404, message, result)
    {
        
    }
    public NotFoundResponse(T? result)
        : base(404, result)
    {
        
    }
}

public class NotFoundResponse : FailedResponse
{
    public NotFoundResponse()
        : base(404)
    {
        
    }
}