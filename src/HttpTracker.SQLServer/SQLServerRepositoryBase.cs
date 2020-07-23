namespace HttpTracker
{
    public abstract class SQLServerRepositoryBase
    {
        public readonly IDbConnectionProvider _dbConnectionProvider;

        protected SQLServerRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }
    }
}