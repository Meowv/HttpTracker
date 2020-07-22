using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker.MySql.Tests
{
    public class TestBase
    {
        public ServiceCollection Services;

        public TestBase()
        {
            Services = new ServiceCollection();

            Services.Configure<HttpTrackerMySqlOptions>(x =>
            {
                x.ConnectionString = "Server=127.0.0.1;User Id=root;Password=123456;Database=meowv;";
            });

            Services.AddSingleton<IDbConnectionProvider, MySqlProvider>();
            Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();
        }
    }
}