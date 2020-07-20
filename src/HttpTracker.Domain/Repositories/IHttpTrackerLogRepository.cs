using HttpTracker.Response;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public interface IHttpTrackerLogRepository
    {
        /// <summary>
        /// 按条件查询HTTP请求跟踪日志数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<HttpTrackerResponse<PagedList<HttpTrackerLog>>> SearchAsync(string type, string keyword, int page, int limit);

        /// <summary>
        /// 插入一条HTTP请求跟踪日志数据
        /// </summary>
        /// <param name="httpTrackerLog"></param>
        /// <returns></returns>
        Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog);
    }
}