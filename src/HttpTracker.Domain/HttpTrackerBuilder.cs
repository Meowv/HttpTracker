using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker
{
    public class HttpTrackerBuilder : IHttpTrackerBuilder
    {
        public HttpTrackerBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }

        public IServiceCollection Services { get; private set; }

        public IConfiguration Configuration { get; private set; }
    }
}