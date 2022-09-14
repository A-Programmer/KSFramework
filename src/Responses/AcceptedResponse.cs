namespace KSFramework.Responses;

public class AcceptedResponse<T> : SuccessResponse<T>
{
    public AcceptedResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}

