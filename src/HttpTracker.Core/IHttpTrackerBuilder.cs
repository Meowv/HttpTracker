using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker
{
    public interface IHttpTrackerBuilder
    {
        IServiceCollection Services { get; }

        IConfiguration Configuration { get; }
    }
}