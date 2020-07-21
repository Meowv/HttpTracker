using HttpTracker.Options;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.Elasticsearch.Tests
{
    public class HttpTrackerElasticsearchProvider_Test
    {
        [Fact]
        public void GetClient()
        {
            var services = new ServiceCollection();

            services.Configure<HttpTrackerElasticsearchOptions>(x =>
            {
                x.Nodes = new string[] { "http://127.0.0.1:9200" };
            });

            services.AddTransient<IElasticsearchProvider, ElasticsearchProvider>();

            var provider = services.BuildServiceProvider().GetRequiredService<IElasticsearchProvider>();
            var client = provider.GetClient();

            Assert.NotNull(client);
        }
    }
}