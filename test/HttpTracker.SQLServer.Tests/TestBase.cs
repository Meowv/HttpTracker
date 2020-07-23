using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker.SQLServer.Tests
{
    public class TestBase
    {
        public ServiceCollection Services;

        public TestBase()
        {
            Services = new ServiceCollection();

            Services.Configure<HttpTrackerSQLServerOptions>(x =>
            {
                x.ConnectionString = "Data Source=.;Initial Catalog=meowv;Persist Security Info=True;User ID=sa;Password=123456";
            });

            Services.AddSingleton<IDbConnectionProvider, SQLServerProvider>();
            Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();
        }
    }
}