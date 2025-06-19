namespace KSFramework.Pagination;

public class OrderingRequestOptions
    : PaginationRequestOptions
{
    public string OrderByPropertyName { get; set; } = "Id";
    public bool Desc { get; set; } = false;
}