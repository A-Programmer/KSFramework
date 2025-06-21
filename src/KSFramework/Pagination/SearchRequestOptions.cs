
using Newtonsoft.Json;

namespace KSFramework.Pagination;

public class SearchRequestOptions
    : OrderingRequestOptions
{
    [property: JsonProperty("searchTerm")] public string SearchTerm { get; set; } = string.Empty;
}