using Microsoft.EntityFrameworkCore;

namespace KSFramework.Utilities
{
    public static class ExtendLinq
    {
        public static async Task<ResponseCollection<TEntity>> AsResponseCollectionAsync<TEntity>(this IQueryable<TEntity> query, RequestCollection request)
        {

            int TotalCount = query.Count();
            List<TEntity> lists = await query.Skip(request.Skip).Take(request.Take).ToListAsync();
            var response = new ResponseCollection<TEntity>()
            {
                Result = lists,
                Count = TotalCount
            };
            return response;
        }

    }
    public class RequestCollection
    {

        private int take;
        public int Take
        {
            get { return take; }
            set
            {
                take = value <= 0 ? 20 : value;
            }
        }
        private int skip;
        public int Skip
        {
            get { return skip; }
            set { skip = value <= 0 ? 0 : value; }
        }
    }
    public class ResponseCollection<TEntity>
    {
        public int Count { get; set; }
        public List<TEntity> Result { get; set; }
    }
}
