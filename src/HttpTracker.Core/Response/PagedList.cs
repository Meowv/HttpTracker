using HttpTracker.Response.Paged;
using System.Collections.Generic;

namespace HttpTracker.Response
{
    public class PagedList<T> : ListResult<T>, IPagedList<T>
    {
        public int Total { get; set; }

        public PagedList()
        {
        }

        public PagedList(int total, IReadOnlyList<T> result) : base(result)
        {
            Total = total;
        }
    }
}