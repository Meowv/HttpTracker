using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseElasticsearch(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerElasticsearchOptions>(builder.Configuration.GetSection("Storage").GetSection("Elasticsearch"));

            return builder.UseElasticsearchService();
        }

        public static IHttpTrackerBuilder UseElasticsearch(this IHttpTrackerBuilder builder, Action<HttpTrackerElasticsearchOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseElasticsearchService();
        }

        internal static IHttpTrackerBuilder UseElasticsearchService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IElasticsearchProvider, ElasticsearchProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}