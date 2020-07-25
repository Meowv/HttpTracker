using HttpTracker.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#if NETSTANDARD2_0 || NETSTANDARD2_1

using Microsoft.AspNetCore.Hosting;

#elif NETCOREAPP3_1

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

            if (httpMethod == "GET" && path.Contains(_options.RoutePrefix) && !Authorize(httpContext))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?{Regex.Escape(_options.RoutePrefix)}/?$"))
            {
                var relativeRedirectPath = $"{path.Split('/').Last()}/index.html";

                RespondWithRedirect(httpContext.Response, relativeRedirectPath);
                return;
            }

            if (httpMethod == "GET" && Regex.IsMatch(path, $"^{Regex.Escape(_options.RoutePrefix)}/index.html$"))
            {
                await RespondWithIndexHtml(httpContext.Response);
                return;
            }

            await _staticFileMiddleware.Invoke(httpContext);

            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound && path.Contains(_options.RoutePrefix))
            {
                var relativeRedirectPath = $"{_options.RoutePrefix}/index.html";

                RespondWithRedirect(httpContext.Response, relativeRedirectPath);
                return;
            }
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
                { "%(BaseUrl)", string.IsNullOrEmpty(_options.RoutePrefix) ? "/": $"/{_options.RoutePrefix.Trim('/')}/" },
            };
        }

        private bool Authorize(HttpContext context)
        {
            if (!_options.OpenBasicAuth) return true;

            var header = context.Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(header))
            {
                var authHeaderValue = AuthenticationHeaderValue.Parse(header);

                if ("Basic".Equals(authHeaderValue.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderValue.Parameter));

                    var parameters = parameter.Split(':');

                    if (parameters.Length > 1)
                    {
                        var username = parameters.First();
                        var password = parameters.Last();

                        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                        {
                            return Validate(username, password);
                        }
                    }
                }
            }

            return Challenge(context);
        }

        private bool Validate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            return username.Equals(_options.Username) && password.Equals(_options.Password);
        }

        private bool Challenge(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"HttpTracker Dashboard\"");
            return false;
        }
    }
}