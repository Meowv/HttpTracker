using MongoDB.Driver;

namespace HttpTracker
{
    public abstract class MongoDbRepositoryBase
    {
        private readonly IMongoDbProvider _mongoDbProvider;

        protected MongoDbRepositoryBase(IMongoDbProvider mongoDbProvider)
        {
            _mongoDbProvider = mongoDbProvider;
        }

        protected abstract string CollectionName { get; }

        public IMongoClient Client => _mongoDbProvider.Client;

        public IMongoDatabase Database => _mongoDbProvider.Database;
    }
}