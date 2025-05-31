using System.Linq.Expressions;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;

namespace KSFramework.Pagination;
public class PaginatedList<T> : List<T>
{
    protected PaginatedList()
    {
    }
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalItems { get; private set; }
    public bool ShowPagination { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;
        ShowPagination = (count > pageSize);
        this.AddRange(items);
    }
    public bool HasPreviousPage
    {
        get
        {
            return (PageIndex > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return (PageIndex < TotalPages);
        }
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize,Expression<Func<T, bool>> where = null,
                                                            string orderBy = "", bool desc = false)
    {
        if(where is null) where = x => true;
        
        if(orderBy.HasValue())
        {
            if(desc)
            {
                source = source.OrderByDescending<T>(orderBy);
            }
            else
            {
                source = source.OrderBy<T>(orderBy);
            }
        }
        
        var count = await source.CountAsync(where);
        var items = await source.Where(where).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize,Expression<Func<T, bool>> where = null,
                                            string orderBy = "", bool desc = false)
    {
        if(where is null) where = x => true;
        
        if(orderBy.HasValue())
        {
            if(desc)
            {
                source = source.OrderByDescending<T>(orderBy);
            }
            else
            {
                source = source.OrderBy<T>(orderBy);
            }
        }
        
        
        var count = source.Count(where);
        var items = source.Where(where).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

}