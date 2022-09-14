namespace KSFramework.Responses;

public class CreatedResponse<T> : SuccessResponse<T>
{
    public CreatedResponse(int code, T? result)
        : base(code, result)
    {
        
    }
}

