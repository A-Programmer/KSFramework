using Newtonsoft.Json;

namespace KSFramework.Pagination;

public class OrderingRequestOptions
    : PaginationRequestOptions
{
    [property:JsonProperty("orderByPropertyName")]
    public string OrderByPropertyName { get; set; } = "Id";
    
    [property:JsonProperty("desc")]
    public bool Desc { get; set; } = false;
}