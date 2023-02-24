using System.ComponentModel.DataAnnotations;

namespace KSFramework.Enums;

public enum ApiResultStatusCode
{
    [Display(Name = "Successfull")]
    Success = 200,

    [Display(Name = "Server Error")]
    ServerError = 500,

    [Display(Name = "Bad Request")]
    BadRequest = 400,

    [Display(Name = "Not Found")]
    NotFound = 404,

    [Display(Name = "Empty List")]
    ListEmpty = 404,

    [Display(Name = "Processing Error")]
    LogicError = 500,

    [Display(Name = "Record Exists")]
    ExistingRecord = 501,

    [Display(Name = "Unauthorized Access")]
    UnAuthorized = 403,

    [Display(Name = "Authentication Error")]
    AuthenticationFailed = 401
}