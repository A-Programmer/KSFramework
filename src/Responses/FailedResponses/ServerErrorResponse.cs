namespace KSFramework.Responses.FailedResponses;

public class ServerErrorResponse<T> : FailedResponse<T>
{
    public ServerErrorResponse(string message, T? result)
        : base(500, message, result)
    {
        
    }
    public ServerErrorResponse(T? result)
        : base(500, result)
    {
        
    }
}

public class ServerErrorResponse : FailedResponse
{
    public ServerErrorResponse()
        : base(500)
    {
        
    }
}