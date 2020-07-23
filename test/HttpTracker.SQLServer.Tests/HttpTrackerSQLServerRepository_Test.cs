using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.SQLServer.Tests
{
    public class HttpTrackerSQLServerRepository_Test : TestBase
    {
        [Fact]
        public void CreateInstance()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            Assert.NotNull(repository);
        }
    }
}