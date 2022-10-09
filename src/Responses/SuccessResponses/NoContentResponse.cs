namespace KSFramework.Responses;

public class NoContentResponse<T> : BaseResponse<T>
{
    public NoContentResponse(T? result)
        : base(204, result)
    {
        
    }
}



public class NoContentResponse : BaseResponse
{
    public NoContentResponse()
        : base(204)
    {
        
    }
}

