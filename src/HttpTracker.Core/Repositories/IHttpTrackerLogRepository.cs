using HttpTracker.Domain;
using HttpTracker.Dto;
using HttpTracker.Dto.Params;
using HttpTracker.Response;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public interface IHttpTrackerLogRepository
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        Task<HttpTrackerResponse> InitAsync();

        /// <summary>
        /// 按条件查询HTTP请求跟踪日志数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(QueryInput input);

        /// <summary>
        /// 插入一条HTTP请求跟踪日志数据
        /// </summary>
        /// <param name="httpTrackerLog"></param>
        /// <returns></returns>
        Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog);
    }
}