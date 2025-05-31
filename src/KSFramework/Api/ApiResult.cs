using System.Net;
using KSFramework.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KSFramework.Api;

public class ApiResult
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Errors { get; set; }

        public ApiResult(bool isSucceeded, HttpStatusCode statusCode, List<string>? errors = null, string? message = null)
        {
            Status = isSucceeded;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
            Errors = errors;
        }
        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, HttpStatusCode.OK, null);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, HttpStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var errorsList = new List<string>();
            if (result.Value is SerializableError errors)
            {
                errorsList = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            else
            {
                errorsList.Add(result.Value.ToString());
            }
            return new ApiResult(false, HttpStatusCode.BadRequest, errorsList);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, HttpStatusCode.OK, null, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, HttpStatusCode.NotFound, null);
        }

        public static implicit operator ApiResult(UnauthorizedResult result)
        {
            return new ApiResult(false, HttpStatusCode.Unauthorized, null);
        }

        public static implicit operator ApiResult(UnauthorizedObjectResult result)
        {
            var errorsList = new List<string>();
            if (result.Value is SerializableError errors)
            {
                errorsList = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            else
            {
                errorsList.Add(result.Value.ToString());
            }
            return new ApiResult(false, HttpStatusCode.Unauthorized, errorsList, null);
        }
        #endregion
    }


    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSucceeded, HttpStatusCode statusCode, TData data, List<string>? errors = null, string? message = null)
            : base(isSucceeded, statusCode, errors, message)
        {
            Data = data;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, HttpStatusCode.OK, data, null);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, HttpStatusCode.OK, null, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, HttpStatusCode.OK, (TData)result.Value, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, HttpStatusCode.BadRequest, null, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var errorsList = new List<string>();
            if (result.Value is SerializableError errors)
            {
                errorsList = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            else
            {
                errorsList.Add(result.Value.ToString());
            }
            return new ApiResult<TData>(false, HttpStatusCode.BadRequest, null, errorsList);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(true, HttpStatusCode.OK, null, null, result.Content);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, HttpStatusCode.NotFound, null, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            var errors = new List<string>();
            errors.Add(result.Value.ToString());
            return new ApiResult<TData>(false, HttpStatusCode.NotFound, (TData)result.Value, errors);
        }


        public static implicit operator ApiResult<TData>(UnauthorizedResult result)
        {
            return new ApiResult<TData>(false, HttpStatusCode.Unauthorized, null, null);
        }

        public static implicit operator ApiResult<TData>(UnauthorizedObjectResult result)
        {
            var errorsList = new List<string>();
            if (result.Value is SerializableError errors)
            {
                errorsList = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            else
            {
                errorsList.Add(result.Value.ToString());
            }
            return new ApiResult<TData>(false, HttpStatusCode.Unauthorized, null, errorsList);
        }
        #endregion
    }