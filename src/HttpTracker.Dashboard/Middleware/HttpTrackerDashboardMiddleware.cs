using HttpTracker.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

#if NETSTANDARD2_1

using Microsoft.AspNetCore.Hosting;

#elif NETCOREAPP3_1

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

#endif

namespace HttpTracker.Middleware
{
    internal class HttpTrackerDashboardMiddleware
    {
        private const string EmbeddedFileNamespace = "HttpTracker.HttpTrackerDashboard";

        private readonly StaticFileMiddleware _staticFileMiddleware;

        private readonly HttpTrackerDashboardOptions _options;

        public HttpTrackerDashboardMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, IOptions<HttpTrackerDashboardOptions> options, ILoggerFactory loggerFactory)
        {
            _options = options.Value;
            _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (string.Equals(httpContext.Request.Method, "GET") && httpContext.Request.Path.StartsWithSegments(_options.RoutePrefix))
            {
                var path = httpContext.Request.Path.Value;

                if (path.Equals(_options.RoutePrefix, StringComparison.OrdinalIgnoreCase) || (path[^1] == '/' && path.Length == _options.RoutePrefix.Length + 1))
                {
                    httpContext.Response.StatusCode = 302;
                    httpContext.Response.Headers["Location"] = $"{_options.RoutePrefix}/index.html";

                    return;
                }
            }

            await _staticFileMiddleware.Invoke(httpContext);
        }

        private StaticFileMiddleware CreateStaticFileMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = string.IsNullOrEmpty(_options.RoutePrefix) ? string.Empty : _options.RoutePrefix,
                FileProvider = new EmbeddedFileProvider(typeof(HttpTrackerDashboardMiddleware).Assembly, EmbeddedFileNamespace),
            };

            return new StaticFileMiddleware(next, hostingEnv, Microsoft.Extensions.Options.Options.Create(staticFileOptions), loggerFactory);
        }
    }
}