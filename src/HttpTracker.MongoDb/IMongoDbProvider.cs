using MongoDB.Driver;

namespace HttpTracker
{
    public interface IMongoDbProvider
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }
    }
}