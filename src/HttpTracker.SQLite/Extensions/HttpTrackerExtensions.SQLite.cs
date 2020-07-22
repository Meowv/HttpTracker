using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseSQLite(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerSQLiteOptions>(builder.Configuration.GetSection("Storage:SQLite"));

            return builder.UseSQLiteService();
        }

        public static IHttpTrackerBuilder UseSQLite(this IHttpTrackerBuilder builder, Action<HttpTrackerSQLiteOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseSQLiteService();
        }

        internal static IHttpTrackerBuilder UseSQLiteService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IDbConnectionProvider, SQLiteProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}