namespace KSFramework.Responses.SuccessResponses;

public class CreatedResponse<T> : BaseResponse<T>
{
    public CreatedResponse(T? result)
        : base(201, result)
    {
        
    }
}



public class CreatedResponse : BaseResponse
{
    public CreatedResponse()
        : base(201)
    {
        
    }
}

