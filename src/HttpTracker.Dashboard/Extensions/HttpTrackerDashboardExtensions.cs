using HttpTracker.Options;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerDashboardExtensions
    {
        public static IHttpTrackerBuilder UseHttpTrackerDashboard(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerDashboardOptions>(builder.Configuration.GetSection("Storage:Dashboard"));

            return builder.UseHttpTrackerDashboardService();
        }

        public static IHttpTrackerBuilder UseHttpTrackerDashboard(this IHttpTrackerBuilder builder, Action<HttpTrackerDashboardOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.UseHttpTrackerDashboardService();
        }

        internal static IHttpTrackerBuilder UseHttpTrackerDashboardService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddMvcCore(options =>
            {
                options.Conventions.Add(new HttpTrackerApplicationModelConvention());
            }).PartManager.FeatureProviders.Add(new HttpTrackerControllerFeatureProvider());

            return builder;
        }
    }
}