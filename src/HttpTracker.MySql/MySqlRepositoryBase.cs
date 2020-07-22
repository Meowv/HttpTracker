using System.Data;

namespace HttpTracker
{
    public abstract class MySqlRepositoryBase
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        protected MySqlRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }

        public IDbConnection Connection => _dbConnectionProvider.Connection;
    }
}