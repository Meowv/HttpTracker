using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.SQLServer.Tests
{
    public class HttpTrackerSQLServerProvider_Test : TestBase
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