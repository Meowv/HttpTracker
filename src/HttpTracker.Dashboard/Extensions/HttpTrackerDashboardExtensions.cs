using HttpTracker.Middleware;
using HttpTracker.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerDashboardExtensions
    {
        public static IHttpTrackerBuilder AddHttpTrackerDashboard(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.Configure<HttpTrackerDashboardOptions>(builder.Configuration.GetSection("Storage:Dashboard"));

            return builder.AddHttpTrackerDashboardService();
        }

        public static IHttpTrackerBuilder AddHttpTrackerDashboard(this IHttpTrackerBuilder builder, Action<HttpTrackerDashboardOptions> options)
        {
            builder.Services.AddOptions();
            builder.Services.Configure(options);

            return builder.AddHttpTrackerDashboardService();
        }

        public static IApplicationBuilder UseHttpTrackerDashboard(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpTrackerDashboardMiddleware>();

            return app;
        }

        internal static IHttpTrackerBuilder AddHttpTrackerDashboardService(this IHttpTrackerBuilder builder)
        {
            builder.Services.AddMvcCore(options =>
            {
                options.Conventions.Add(new HttpTrackerApplicationModelConvention());
            }).PartManager.FeatureProviders.Add(new HttpTrackerControllerFeatureProvider());

            return builder;
        }
    }
}