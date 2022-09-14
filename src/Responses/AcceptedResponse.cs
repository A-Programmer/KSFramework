namespace KSFramework.Responses;

public class AcceptedResponse<T> : SuccessResponse<T>
{
    public AcceptedResponse(T? result)
        : base(202, result)
    {
        
    }
}

