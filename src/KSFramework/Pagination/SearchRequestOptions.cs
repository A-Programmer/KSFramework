
namespace KSFramework.Pagination;

public class SearchRequestOptions
    : OrderingRequestOptions
{
    public string SearchTerm { get; set; } = string.Empty;
}