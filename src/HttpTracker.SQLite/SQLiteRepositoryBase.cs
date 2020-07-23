namespace HttpTracker
{
    public abstract class SQLiteRepositoryBase
    {
        public readonly IDbConnectionProvider _dbConnectionProvider;

        protected SQLiteRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }
    }
}