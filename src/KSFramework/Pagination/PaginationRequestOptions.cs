using Newtonsoft.Json;

namespace KSFramework.Pagination;

public class PaginationRequestOptions
{
    [property:JsonProperty("pageIndex")]
    public int PageIndex { get; set; } = 1;
    
    [property:JsonProperty("pageSize")]
    public int PageSize { get; set; } = 20;
}