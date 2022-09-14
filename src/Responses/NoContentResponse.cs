namespace KSFramework.Responses;

public class NoContentResponse<T> : SuccessResponse<T>
{
    public NoContentResponse(T? result)
        : base(204, result)
    {
        
    }
}



public class NoContentResponse : SuccessResponse
{
    public NoContentResponse()
        : base(204)
    {
        
    }
}

