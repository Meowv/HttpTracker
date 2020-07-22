using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseOracle(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerOracleOptions>(builder.Configuration.GetSection("Storage:Oracle"));

            return builder.UseOracleService();
        }

        public static IHttpTrackerBuilder UseOracle(this IHttpTrackerBuilder builder, Action<HttpTrackerOracleOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseOracleService();
        }

        internal static IHttpTrackerBuilder UseOracleService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IDbConnectionProvider, OracleProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}