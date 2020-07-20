using HttpTracker.Response;
using System;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepository : ElasticsearchRepositoryBase, IHttpTrackerLogRepository
    {
        public HttpTrackerLogRepository(IElasticsearchProvider elasticsearchProvider) : base(elasticsearchProvider)
        {
            var date = DateTimeOffset.UtcNow;

            IndexName = $"{date.Year}_{date.Month}";
        }

        protected override string IndexName { get; }

        public async Task<HttpTrackerResponse<PagedList<HttpTrackerLog>>> SearchAsync(string type, string keyword, DateTime date, int page, int limit)
        {
            var response = new HttpTrackerResponse<PagedList<HttpTrackerLog>>();


            return response;
        }

        public async Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog)
        {
            var response = new HttpTrackerResponse();
            if (httpTrackerLog == null)
            {
                response.IsFailed("HttpTrackerLog is null");
                return response;
            }

            try
            {
                await Client.IndexAsync(httpTrackerLog, x => x.Index(IndexName));
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

            return response;
        }
    }
}