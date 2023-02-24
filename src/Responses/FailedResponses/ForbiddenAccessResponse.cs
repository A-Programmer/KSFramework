namespace KSFramework.Responses.FailedResponses;

public class ForbiddenAccessResponse<T> : FailedResponse<T>
{
    public ForbiddenAccessResponse(string message, T? result)
        : base(403, message, result)
    {
        
    }
    public ForbiddenAccessResponse(T? result)
        : base(403, result)
    {
        
    }
}

public class ForbiddenAccessResponse : FailedResponse
{
    public ForbiddenAccessResponse()
        : base(403)
    {
        
    }
}