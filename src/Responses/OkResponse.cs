namespace KSFramework.Responses;

public class OkResponse<T> : SuccessResponse<T>
{
    public OkResponse(T? result)
        : base(200, result)
    {
        
    }
}

