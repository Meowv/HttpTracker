using HttpTracker.Options;
using Microsoft.Extensions.Options;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepositoryFactory : IHttpTrackerLogRepositoryFactory
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        private HttpTrackerNpgsqlOptions Options { get; }

        public HttpTrackerLogRepositoryFactory(IDbConnectionProvider dbConnectionProvider, IOptions<HttpTrackerNpgsqlOptions> options)
        {
            _dbConnectionProvider = dbConnectionProvider;
            Options = options.Value;
        }

        public IHttpTrackerLogRepository CreateInstance(string name)
        {
            return new HttpTrackerLogRepository(_dbConnectionProvider, Options, name);
        }
    }
}