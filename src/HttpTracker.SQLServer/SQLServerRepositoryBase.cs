using System.Data;

namespace HttpTracker
{
    public abstract class SQLServerRepositoryBase
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        protected SQLServerRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }

        public IDbConnection Connection => _dbConnectionProvider.Connection;
    }
}