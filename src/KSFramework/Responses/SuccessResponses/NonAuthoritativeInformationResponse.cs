namespace KSFramework.Responses.SuccessResponses;

public class NonAuthoritativeInformationResponse<T> : BaseResponse<T>
{
    public NonAuthoritativeInformationResponse(T? result)
        : base(203, result)
    {
        
    }
}



public class NonAuthoritativeInformationResponse : BaseResponse
{
    public NonAuthoritativeInformationResponse()
        : base(203)
    {
        
    }
}

