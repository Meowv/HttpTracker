using HttpTracker.Options;
using Microsoft.Extensions.Options;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepositoryFactory : IHttpTrackerLogRepositoryFactory
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public HttpTrackerLogRepositoryFactory(IDbConnectionProvider dbConnectionProvider, IOptions<HttpTrackerSQLiteOptions> options)
        {
            _dbConnectionProvider = dbConnectionProvider;
            Options = options.Value;
        }

        private HttpTrackerSQLiteOptions Options { get; }

        public IHttpTrackerLogRepository CreateInstance(string name)
        {
            return new HttpTrackerLogRepository(_dbConnectionProvider, Options, name);
        }
    }
}