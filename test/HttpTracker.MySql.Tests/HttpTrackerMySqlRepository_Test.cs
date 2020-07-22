using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.MySql.Tests
{
    public class HttpTrackerMySqlRepository_Test : TestBase
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