using System.Data;

namespace HttpTracker
{
    public abstract class OracleRepositoryBase
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        protected OracleRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }

        public IDbConnection Connection => _dbConnectionProvider.Connection;
    }
}