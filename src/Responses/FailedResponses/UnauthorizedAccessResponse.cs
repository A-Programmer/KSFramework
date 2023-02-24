namespace KSFramework.Responses.FailedResponses;

public class UnauthorizedAccessResponse<T> : FailedResponse<T>
{
    public UnauthorizedAccessResponse(string message, T? result)
        : base(401, message, result)
    {
        
    }
    public UnauthorizedAccessResponse(T? result)
        : base(401, result)
    {
        
    }
}

public class UnauthorizedAccessResponse : FailedResponse
{
    public UnauthorizedAccessResponse()
        : base(401)
    {
        
    }
}