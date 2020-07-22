using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
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

        [Fact]
        public async Task InitAsync()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            Assert.NotNull(repository);

            var response = await repository.InitAsync();

            Assert.True(response.Success);
        }
    }
}