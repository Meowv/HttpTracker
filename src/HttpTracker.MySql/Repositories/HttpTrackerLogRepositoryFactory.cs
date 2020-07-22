using HttpTracker.Options;
using Microsoft.Extensions.Options;
using System;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepositoryFactory : IHttpTrackerLogRepositoryFactory
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        private HttpTrackerMySqlOptions Options { get; }

        public HttpTrackerLogRepositoryFactory(IDbConnectionProvider dbConnectionProvider, IOptions<HttpTrackerMySqlOptions> options)
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