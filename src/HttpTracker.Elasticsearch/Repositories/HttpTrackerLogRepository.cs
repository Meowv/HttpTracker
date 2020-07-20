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

        public Task<HttpTrackerResponse<PagedList<HttpTrackerLog>>> SearchAsync(string type, string keyword, DateTime date, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog)
        {
            throw new NotImplementedException();
        }
    }
}