using Microsoft.Extensions.Options;

namespace HttpTracker.Options
{
    public class HttpTrackerDashboardOptions : IOptions<HttpTrackerDashboardOptions>
    {
        public string RoutePrefix { get; set; } = "httptracker";

        public string DocumentTitle { get; set; } = "HttpTracker Dashboard";

        public string Username { get; set; } = "meowv";

        public string Password { get; set; } = "123456";

        public HttpTrackerDashboardOptions Value => this;
    }
}