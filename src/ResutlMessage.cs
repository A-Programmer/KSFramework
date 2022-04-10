using System;
using Newtonsoft.Json;

namespace KSFramework
{
    public class ResultMessage
    {
        public bool IsSuccess { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }


        protected ResultMessage()
        {

        }
        public ResultMessage(bool isSuccess, Status status, string message)
        {
            IsSuccess = isSuccess;
            Status = status;
            Message = message;
        }
    }

    public class ResultMessage<TData> : ResultMessage
    {

        public ResultMessage(bool isSuccess, TData data, Status status, string message) : base(isSuccess, status, message)
        {
            Data = data;

        }

        public ResultMessage(bool isSuccess,TData data, Status status, string message, int? pageIndex, int? totalPages, int? totalItems,
            bool? showPagination) : base(isSuccess, status, message)
        {
            Data = data;
            PageIndex = pageIndex;
            TotalPages = totalPages;
            TotalItems = totalItems;
            ShowPagination = showPagination;

        }


        public TData Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PageIndex { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalPages { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalItems { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowPagination { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasPreviousPage
        {
            get
            {
                if (ShowPagination == null || ShowPagination == false)
                    return null;

                PageIndex = PageIndex ?? 0;
                return (PageIndex > 1);
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasNextPage
        {
            get
            {
                if (ShowPagination == null || ShowPagination == false)
                    return null;

                PageIndex = PageIndex ?? 0;
                TotalPages = TotalPages ?? 0;
                return (PageIndex < TotalPages);
            }
        }
    }

    public enum Status
    {
        Success = 200,
        BadRequest = 400,
        NotFound = 404,
        ServerError = 500,
        NotAuthenticated = 401,
        NotAuthorized = 403,
        DuplicateRecord = 405
    }
}
