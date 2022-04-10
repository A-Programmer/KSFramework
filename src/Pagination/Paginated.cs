using System;
namespace KSFramework.Pagination
{
    public class Paginated
    {
        public Paginated(int? pageNumber, int? pageSize)
        {
            PageIndex = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
        }


        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
    }
}
