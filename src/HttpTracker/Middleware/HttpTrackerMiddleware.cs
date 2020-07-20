using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HttpTracker.Middleware
{
    public class HttpTrackerMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpTrackerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}