using HttpTracker.Options;
using Microsoft.Extensions.Options;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepositoryFactory : IHttpTrackerLogRepositoryFactory
    {
        private readonly IElasticsearchProvider _elasticsearchProvider;
        private HttpTrackerElasticsearchOptions Options { get; }

        public HttpTrackerLogRepositoryFactory(IElasticsearchProvider elasticsearchProvider, IOptions<HttpTrackerElasticsearchOptions> options)
        {
            _elasticsearchProvider = elasticsearchProvider;
            Options = options.Value;
        }

        public IHttpTrackerLogRepository CreateInstance(string yearMonth)
        {
            return new HttpTrackerLogRepository(_elasticsearchProvider, Options, yearMonth);
        }
    }
}