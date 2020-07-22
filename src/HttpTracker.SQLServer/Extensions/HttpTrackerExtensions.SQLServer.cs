using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseSQLServer(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerSQLServerOptions>(builder.Configuration.GetSection("Storage:SQLServer"));

            return builder.UseSQLServerService();
        }

        public static IHttpTrackerBuilder UseSQLServer(this IHttpTrackerBuilder builder, Action<HttpTrackerSQLServerOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseSQLServerService();
        }

        internal static IHttpTrackerBuilder UseSQLServerService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IDbConnectionProvider, SQLServerProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}