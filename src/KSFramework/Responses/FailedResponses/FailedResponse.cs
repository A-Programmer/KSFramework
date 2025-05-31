namespace KSFramework.Responses.FailedResponses;

public abstract class FailedResponse<T> : BaseResponse<T>
{
    public string Message { get; set; }
    public T? Errors { get; set; }

    public FailedResponse(int code)
        : base(code)
    {
    }

    public FailedResponse(int code, T? errors)
        : base(code)
    {
        Errors = errors;
    }

    public FailedResponse(int code, string message)
        : base(code)
    {
        Message = message;
    }

    public FailedResponse(int code, string message, T? errors)
        : base(code)
    {
        Message = message;
        Errors = errors;
    }
}

public abstract class FailedResponse : BaseResponse
{
    public string Message { get; set; }

    public FailedResponse(int code)
        : base(code)
    {
    }
    public FailedResponse(int code, string message)
        : base(code)
    {
        Message = message;
    }
}