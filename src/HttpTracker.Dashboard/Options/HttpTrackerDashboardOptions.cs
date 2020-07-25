using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Reflection;

namespace HttpTracker.Options
{
    public class HttpTrackerDashboardOptions : IOptions<HttpTrackerDashboardOptions>
    {
        public string RoutePrefix { get; set; } = "/httptracker";

        public string DocumentTitle { get; set; } = "✨ HttpTracker Dashboard";

        public bool OpenBasicAuth { get; set; } = true;

        public string Username { get; set; } = "httptracker";

        public string Password { get; set; } = "httptracker";

        public Func<Stream> IndexStream { get; set; } = () => typeof(HttpTrackerDashboardOptions).GetTypeInfo().Assembly.GetManifestResourceStream("HttpTracker.Blazor.index.html");

        public HttpTrackerDashboardOptions Value => this;
    }
}