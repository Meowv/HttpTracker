namespace HttpTracker.Response.Paged
{
    public interface IPagedList<T> : IListResult<T>, IHasTotalCount
    {
    }
}