using HttpTracker.Options;
using Microsoft.Extensions.Options;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepositoryFactory : IHttpTrackerLogRepositoryFactory
    {
        private readonly IMongoDbProvider _mongoDbProvider;

        private HttpTrackerMongoDbOptions Options { get; }

        public HttpTrackerLogRepositoryFactory(IMongoDbProvider mongoDbProvider, IOptions<HttpTrackerMongoDbOptions> options)
        {
            _mongoDbProvider = mongoDbProvider;
            Options = options.Value;
        }

        public IHttpTrackerLogRepository CreateInstance(string name)
        {
            return new HttpTrackerLogRepository(_mongoDbProvider, Options, name);
        }
    }
}