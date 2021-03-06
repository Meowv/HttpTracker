﻿using HttpTracker.Filters;
using HttpTracker.Middleware;
using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace HttpTracker.Extensions
{
    public static class HttpTrackerExtensions
    {
        public static IHttpTrackerBuilder AddHttpTracker(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("HttpTracker");

            services.AddOptions();
            services.Configure<HttpTrackerOptions>(configuration);

            return services.AddHttpTrackerService(configuration);
        }

        public static IHttpTrackerBuilder AddHttpTracker(this IServiceCollection services, Action<HttpTrackerOptions> options)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("HttpTracker");

            services.AddOptions();
            services.Configure(options);

            return services.AddHttpTrackerService(configuration);
        }

        internal static IHttpTrackerBuilder AddHttpTrackerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvcCore(x =>
            {
                x.Filters.Add<HttpTrackerExceptionFilter>();
            });

            return new HttpTrackerBuilder(services, configuration);
        }

        public static IApplicationBuilder UseHttpTracker(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<HttpTrackerOptions>>();
            if (options.Value.Disabled) return null;

            var factory = app.ApplicationServices.GetService<IHttpTrackerLogRepositoryFactory>();

            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            repository.InitAsync();

            app.UseMiddleware<HttpTrackerMiddleware>();

            return app;
        }
    }
}