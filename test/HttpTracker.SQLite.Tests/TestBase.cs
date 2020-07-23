using HttpTracker.Options;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HttpTracker.SQLite.Tests
{
    public class TestBase
    {
        public ServiceCollection Services;

        public TestBase()
        {
            Services = new ServiceCollection();

            Services.Configure<HttpTrackerSQLiteOptions>(x =>
            {
                x.ConnectionString = "Data Source=D:/meowv.db;";
            });

            Services.AddSingleton<IDbConnectionProvider, SQLiteProvider>();
            Services.AddSingleton<IHttpTrackerLogRepositoryFactory, HttpTrackerLogRepositoryFactory>();
        }
    }
}