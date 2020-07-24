using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Reflection;

namespace HttpTracker.Options
{
    public class HttpTrackerDashboardOptions : IOptions<HttpTrackerDashboardOptions>
    {
        public string RoutePrefix { get; set; } = "/httptracker";

        public string DocumentTitle { get; set; } = "HttpTracker Dashboard";

        public string Username { get; set; } = "meowv";

        public string Password { get; set; } = "123456";

        public Func<Stream> IndexStream { get; set; } = () => typeof(HttpTrackerDashboardOptions).GetTypeInfo().Assembly.GetManifestResourceStream("HttpTracker.Blazor.index.html");

        public HttpTrackerDashboardOptions Value => this;
    }
}