using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseMongoDb(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerMongoDbOptions>(builder.Configuration.GetSection("Storage:MongoDb"));

            return builder.UseMongoDbService();
        }

        public static IHttpTrackerBuilder UseMongoDb(this IHttpTrackerBuilder builder, Action<HttpTrackerMongoDbOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseMongoDbService();
        }

        internal static IHttpTrackerBuilder UseMongoDbService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IMongoDbProvider, MongoDbProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}