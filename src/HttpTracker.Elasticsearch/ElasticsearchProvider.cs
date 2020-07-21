using Elasticsearch.Net;
using HttpTracker.Options;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Linq;

namespace HttpTracker
{
    public class ElasticsearchProvider : IElasticsearchProvider
    {
        public HttpTrackerElasticsearchOptions Options { get; }

        public ElasticsearchProvider(IOptions<HttpTrackerElasticsearchOptions> options)
        {
            Options = options.Value;
        }

        public IElasticClient GetClient()
        {
            if (Options.Nodes == null)
                throw new Exception("Elasticsearch 配置有误，请检查。");

            ConnectionSettings connectionSettings;

            if (Options.Nodes.Count() > 1)
            {
                var nodes = Options.Nodes.Select(s => new Uri(s)).ToList();
                var connectionPool = new StaticConnectionPool(nodes);

                connectionSettings = new ConnectionSettings(connectionPool);
            }
            else
            {
                var node = Options.Nodes.Select(s => new Uri(s)).FirstOrDefault();

                connectionSettings = new ConnectionSettings(node);
            }

            connectionSettings.BasicAuthentication(Options.Username, Options.Password);

            return new ElasticClient(connectionSettings);
        }
    }
}