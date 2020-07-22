using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder UseMySql(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerMySqlOptions>(builder.Configuration.GetSection("Storage").GetSection("MySql"));

            return builder.UseMySqlService();
        }

        public static IHttpTrackerBuilder UseMySql(this IHttpTrackerBuilder builder, Action<HttpTrackerMySqlOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseMySqlService();
        }

        public static IHttpTrackerBuilder UseMySqlService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddSingleton<IDbConnectionProvider, MySqlProvider>();
            builder.Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();

            return builder;
        }
    }
}