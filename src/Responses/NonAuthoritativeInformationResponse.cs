namespace KSFramework.Responses;

public class NonAuthoritativeInformationResponse<T> : SuccessResponse<T>
{
    public NonAuthoritativeInformationResponse(T? result)
        : base(203, result)
    {
        
    }
}



public class NonAuthoritativeInformationResponse : SuccessResponse
{
    public NonAuthoritativeInformationResponse()
        : base(203)
    {
        
    }
}

