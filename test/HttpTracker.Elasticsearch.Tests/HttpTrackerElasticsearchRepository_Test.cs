using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.Elasticsearch.Tests
{
    public class HttpTrackerElasticsearchRepository_Test : TestBase
    {
        [Fact]
        public void CreateInstance()
        {
            Services.AddSingleton<IElasticsearchProvider, ElasticsearchProvider>();
            Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();

            var repository = factory.CreateInstance(yearMonth);

            Assert.NotNull(repository);
        }
    }
}