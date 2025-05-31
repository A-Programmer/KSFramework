namespace KSFramework.Responses.SuccessResponses;

public class OkResponse<T> : BaseResponse<T>
{
    public OkResponse(T? result)
        : base(200, result)
    {
        
    }
}



public class OkResponse : BaseResponse
{
    public OkResponse()
        : base(200)
    {
        
    }
}

