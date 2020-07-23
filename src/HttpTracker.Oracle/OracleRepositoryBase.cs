namespace HttpTracker
{
    public abstract class OracleRepositoryBase
    {
        public readonly IDbConnectionProvider _dbConnectionProvider;

        protected OracleRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        protected abstract string TableName { get; }
    }
}