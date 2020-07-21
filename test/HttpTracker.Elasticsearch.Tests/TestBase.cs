using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker.Elasticsearch.Tests
{
    public class TestBase
    {
        public ServiceCollection Services;

        public TestBase()
        {
            Services = new ServiceCollection();

            Services.Configure<HttpTrackerElasticsearchOptions>(x =>
            {
                x.Nodes = new string[] { "http://127.0.0.1:9200" };
            });

            Services.AddSingleton<IElasticsearchProvider, ElasticsearchProvider>();
            Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();
        }
    }
}