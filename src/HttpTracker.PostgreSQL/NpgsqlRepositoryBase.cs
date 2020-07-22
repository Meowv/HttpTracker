using System.Data;

namespace HttpTracker
{
    public abstract class NpgsqlRepositoryBase
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        protected NpgsqlRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }

        public IDbConnection Connection => _dbConnectionProvider.Connection;
    }
}