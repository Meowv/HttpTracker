namespace HttpTracker
{
    public abstract class MySqlRepositoryBase
    {
        public readonly IDbConnectionProvider _dbConnectionProvider;

        protected MySqlRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }
    }
}