namespace KSFramework.Responses.FailedResponses;

public class FormValidationResponse<T> : FailedResponse<T>
{
    public FormValidationResponse(string message, T? result)
        : base(400, message, result)
    {
        
    }
    public FormValidationResponse(T? result)
        : base(400, result)
    {
        
    }
}

public class FormValidationResponse : FailedResponse
{
    public FormValidationResponse()
        : base(400)
    {
        
    }
}