
using Newtonsoft.Json;

namespace KSFramework.Pagination;

public record SearchRequestOptions
    : OrderingRequestOptions
{
    [property:JsonProperty("searchTerm")]
    public string SearchTerm { get; set; }
}