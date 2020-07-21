using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.Elasticsearch.Tests
{
    public class HttpTrackerElasticsearchProvider_Test : TestBase
    {
        [Fact]
        public void GetClient()
        {
            Services.AddTransient<IElasticsearchProvider, ElasticsearchProvider>();

            var provider = Services.BuildServiceProvider().GetRequiredService<IElasticsearchProvider>();
            var client = provider.GetClient();

            Assert.NotNull(client);
        }
    }
}