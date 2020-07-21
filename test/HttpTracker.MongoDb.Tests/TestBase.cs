using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker.MongoDb.Tests
{
    public class TestBase
    {
        public ServiceCollection Services;

        public TestBase()
        {
            Services = new ServiceCollection();

            Services.Configure<HttpTrackerMongoDbOptions>(x =>
            {
                x.Services = new string[] { "127.0.0.1:27017" };
                x.DatabaseName = "meowv";
            });

            Services.AddSingleton<IMongoDbProvider, MongoDbProvider>();
            Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();
        }
    }
}