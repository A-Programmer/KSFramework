namespace KSFramework.Responses;

public class NonAuthoritativeInformationResponse<T> : SuccessResponse<T>
{
    public NonAuthoritativeInformationResponse(T? result)
        : base(203, result)
    {
        
    }
}

