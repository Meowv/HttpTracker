using HttpTracker.Attributes;
using HttpTracker.Domain;
using HttpTracker.Extensions;
using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HttpTracker.Middleware
{
    /// <summary>
    /// HttpTracker Middleware
    /// </summary>
    public class HttpTrackerMiddleware
    {
        private readonly RequestDelegate _next;

        private HttpTrackerOptions Options { get; }

        private readonly IHttpTrackerLogRepositoryFactory _factory;

        private HttpTrackerLog log;

        public HttpTrackerMiddleware(RequestDelegate next, IOptions<HttpTrackerOptions> options, IHttpTrackerLogRepositoryFactory factory)
        {
            _next = next;
            Options = options.Value;
            _factory = factory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;

            if (endpoint == null || FilterRequest(context) || DisabledRequestTracker(endpoint, out string description))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            try
            {
                await _next(context);

                stopwatch.Stop();

                log = new HttpTrackerLog()
                {
                    Description = description,
                    UserAgent = context.GetUserAgent(),
                    Method = context.GetHttpMethod(),
                    Url = context.GetAbsoluteUri(),
                    Referrer = context.GetReferer(),
                    IpAddress = context.GetIpAddress(),
                    Milliseconds = Convert.ToInt32(stopwatch.ElapsedMilliseconds),
                    QueryString = context.GetQueryString(),
                    RequestBody = await context.GetRequestBodyAsync(),
                    Cookies = context.GetCookies(),
                    Headers = context.GetHeaders(),
                    StatusCode = context.GetStatusCode(),
                    ResponseBody = await context.GetResponseBodyAsync(),
                    ServerName = Options.ServerName,
                    PId = Process.GetCurrentProcess()?.Id,
                    Host = Options.ServerHost,
                    Port = Options.ServerPort
                };
            }
            catch (Exception ex)
            {
                log.ExceptionType = ex?.GetType().ToString();
                log.Message = ex?.Message;
                log.StackTrace = ex?.StackTrace;
            }
            finally
            {
                var repository = _factory.CreateInstance(HttpTrackerInstance.InstanceName);
                await repository.InsertAsync(log);
            }
        }

        private bool FilterRequest(HttpContext context)
        {
            var result = false;

            if (Options.FilterRequest == null)
            {
                return result;
            }

            var path = context.Request.Path.Value.ToLowerInvariant();

            result = Options.FilterRequest.Any(x => path.StartsWith(x.ToLowerInvariant()));

            return result;
        }

        private bool DisabledRequestTracker(Endpoint endpoint, out string description)
        {
            var result = false;

            if (endpoint != null)
            {
                if (endpoint.Metadata
                            .GetMetadata<ControllerActionDescriptor>()?
                            .MethodInfo
                            .GetCustomAttribute(typeof(HttpTrackerAttribute)) is HttpTrackerAttribute attribute)
                {
                    description = attribute.Description;
                    return attribute.Disabled;
                }
            }

            description = null;
            return result;
        }
    }
}