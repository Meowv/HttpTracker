using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseNpgsql(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerNpgsqlOptions>(builder.Configuration.GetSection("Storage:PostgreSQL"));

            return builder.UseNpgsqlService();
        }

        public static IHttpTrackerBuilder UseNpgsql(this IHttpTrackerBuilder builder, Action<HttpTrackerNpgsqlOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseNpgsqlService();
        }

        internal static IHttpTrackerBuilder UseNpgsqlService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IDbConnectionProvider, NpgsqlProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}