using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.MongoDb.Tests
{
    public class HttpTrackerMongoDbProvider_Test : TestBase
    {
        [Fact]
        public void GetClient()
        {
            var provider = Services.BuildServiceProvider().GetRequiredService<IMongoDbProvider>();

            var client = provider.Client;
            var database = provider.Database;

            Assert.NotNull(client);
            Assert.NotNull(database);
        }
    }
}