using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpTracker.MySql.Tests
{
    public class HttpTrackerMySqlProvider_Test : TestBase
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