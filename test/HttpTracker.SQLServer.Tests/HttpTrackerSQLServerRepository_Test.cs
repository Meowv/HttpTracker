using HttpTracker.Domain;
using HttpTracker.Dto.Params;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace HttpTracker.SQLServer.Tests
{
    public class HttpTrackerSQLServerRepository_Test : TestBase
    {
        [Fact]
        public void CreateInstance()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            Assert.NotNull(repository);
        }

        [Fact]
        public async Task InitAsync()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            Assert.NotNull(repository);

            var response = await repository.InitAsync();

            Assert.True(response.Success);
        }

        [Fact]
        public async Task InsertAsync()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            Assert.NotNull(repository);

            var log = new HttpTrackerLog
            {
                Type = HttpTrackerLog.Types.Debug,
                Description = "this is a test",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.85 Safari/537.36 Edg/84.0.522.35",
                Method = "GET",
                Url = "https://meowv.com",
                Referrer = "https://meowv.com",
                IpAddress = "192.168.1.111",
                Milliseconds = new Random().Next(1, 9999),
                QueryString = "",
                RequestBody = "",
                Cookies = "",
                Headers = "",
                StatusCode = 200,
                ResponseBody = "我是返回数据",
                ServerName = "test",
                PId = Process.GetCurrentProcess()?.Id,
                Host = "192.168.1.1",
                Port = 5000,
                ExceptionType = "",
                Message = "",
                StackTrace = ""
            };

            var response = await repository.InsertAsync(log);

            Assert.True(response.Success);
        }

        [Fact]
        public async Task QueryAsync()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(HttpTrackerInstance.InstanceName);

            Assert.NotNull(repository);

            var input = new QueryInput
            {
                Type = "",
                Keyword = "",
                Page = 1,
                Limit = 20
            };

            var response = await repository.QueryAsync(input);

            Assert.True(response.Success);
            Assert.True(response.Result.Total > 0);
            Assert.NotEmpty(response.Result.Item);
        }
    }
}