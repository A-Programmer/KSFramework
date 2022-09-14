namespace KSFramework.Responses;

public class NonAuthoritativeInformationResponse<T> : SuccessResponse<T>
{
    public NonAuthoritativeInformationResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}

