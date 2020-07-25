using HttpTracker.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#if NETCOREAPP3_1
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
#endif

namespace HttpTracker.Middleware
{
    internal class HttpTrackerDashboardMiddleware
    {
        private const string EmbeddedFileNamespace = "HttpTracker.Blazor";

        private readonly StaticFileMiddleware _staticFileMiddleware;

        private readonly HttpTrackerDashboardOptions _options;

        public HttpTrackerDashboardMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, IOptions<HttpTrackerDashboardOptions> options, ILoggerFactory loggerFactory)
        {
            _options = options.Value;
            _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path.Value;

            if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?{Regex.Escape(_options.RoutePrefix)}/?$"))
            {
                var relativeRedirectPath = path.EndsWith("/") ? "index.html" : $"{path.Split('/').Last()}/index.html";

                RespondWithRedirect(httpContext.Response, relativeRedirectPath);
                return;
            }

            if (httpMethod == "GET" && Regex.IsMatch(path, $"^{Regex.Escape(_options.RoutePrefix)}/index.html$"))
            {
                await RespondWithIndexHtml(httpContext.Response);
                return;
            }

            await _staticFileMiddleware.Invoke(httpContext);
        }

        private StaticFileMiddleware CreateStaticFileMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = string.IsNullOrEmpty(_options.RoutePrefix) ? string.Empty : _options.RoutePrefix,
                FileProvider = new EmbeddedFileProvider(typeof(HttpTrackerDashboardMiddleware).Assembly, EmbeddedFileNamespace),
                ContentTypeProvider = ContentTypeProvider()
            };

            return new StaticFileMiddleware(next, hostingEnv, Microsoft.Extensions.Options.Options.Create(staticFileOptions), loggerFactory);
        }

        private void RespondWithRedirect(HttpResponse response, string location)
        {
            response.StatusCode = 301;
            response.Headers["Location"] = location;
        }

        private async Task RespondWithIndexHtml(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html;charset=utf-8";

            using var stream = _options.IndexStream();

            var htmlBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
            foreach (var entry in GetIndexArguments())
            {
                htmlBuilder.Replace(entry.Key, entry.Value);
            }

            await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
        }

        private FileExtensionContentTypeProvider ContentTypeProvider()
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".dll"] = "application/octet-stream";
            provider.Mappings[".dat"] = "application/octet-stream";
            provider.Mappings[".pdb"] = "application/octet-stream";
            provider.Mappings[".wasm"] = "application/wasm";

            return provider;
        }

        private IDictionary<string, string> GetIndexArguments()
        {
            return new Dictionary<string, string>
            {
                { "%(DocumentTitle)", _options.DocumentTitle },
            };
        }
    }
}