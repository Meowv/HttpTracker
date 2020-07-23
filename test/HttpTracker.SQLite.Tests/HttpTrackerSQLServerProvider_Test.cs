using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.SQLite.Tests
{
    public class HttpTrackerSQLiteProvider_Test : TestBase
    {
        [Fact]
        public void GetConnection()
        {
            var provider = Services.BuildServiceProvider().GetRequiredService<IDbConnectionProvider>();

            var connection = provider.Connection;

            Assert.NotNull(connection);
        }
    }
}