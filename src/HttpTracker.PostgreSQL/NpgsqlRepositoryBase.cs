namespace HttpTracker
{
    public abstract class NpgsqlRepositoryBase
    {
        public readonly IDbConnectionProvider _dbConnectionProvider;

        protected NpgsqlRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }
    }
}