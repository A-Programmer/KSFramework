using System;
using System.ComponentModel.DataAnnotations;

namespace KSFramework.Enums
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 200,

        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 500,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 400,

        [Display(Name = "یافت نشد")]
        NotFound = 404,

        [Display(Name = "لیست خالی است")]
        ListEmpty = 404,

        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 500,

        [Display(Name = "داده ای با این مشخصات موجود می باشد")]
        ExistingRecord = 501,

        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 403,

        [Display(Name = "خطا در احراز هویت")]
        AuthenticationFailed = 401
    }
}
