namespace KSFramework.Responses;

public class CreatedResponse<T> : SuccessResponse<T>
{
    public CreatedResponse(T? result)
        : base(201, result)
    {
        
    }
}

