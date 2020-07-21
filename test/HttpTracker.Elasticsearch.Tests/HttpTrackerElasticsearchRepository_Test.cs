using HttpTracker.Domain;
using HttpTracker.Domain.Data;
using HttpTracker.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace HttpTracker.Elasticsearch.Tests
{
    public class HttpTrackerElasticsearchRepository_Test : TestBase
    {
        [Fact]
        public void CreateInstance()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(yearMonth);

            Assert.NotNull(repository);
        }

        [Fact]
        public async Task InsertAsync()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(yearMonth);

            Assert.NotNull(repository);

            var log = new HttpTrackerLog
            {
                YearMonth = yearMonth,
                Type = HttpTrackerLog.Types.Debug,
                Description = "this is a test",
            };

            var requestInfo = new RequestInfo
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.85 Safari/537.36 Edg/84.0.522.35",
                Method = "GET",
                Url = "https://meowv.com",
                Referrer = "https://meowv.com",
                IpAddress = "192.168.1.111",
                Milliseconds = new Random().Next(1, 9999),
                QueryString = "",
                Body = "",
                Cookies = "",
                Headers = ""
            };

            var responseInfo = new ResponseInfo
            {
                StatusCode = 200,
                Body = "我是返回数据"
            };

            var serviceInfo = new ServerInfo
            {
                Name = "test",
                PId = Process.GetCurrentProcess()?.Id,
                Host = "192.168.1.1",
                Port = 5000
            };

            var exceptionInfo = new ExceptionInfo();

            log.Data[HttpTrackerLog.DataKeys.Request] = requestInfo;
            log.Data[HttpTrackerLog.DataKeys.Response] = responseInfo;
            log.Data[HttpTrackerLog.DataKeys.Server] = serviceInfo;
            log.Data[HttpTrackerLog.DataKeys.Exception] = exceptionInfo;

            var response = await repository.InsertAsync(log);

            Assert.True(response.Success);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var factory = Services.BuildServiceProvider().GetRequiredService<IHttpTrackerLogRepositoryFactory>();
            var repository = factory.CreateInstance(yearMonth);

            Assert.NotNull(repository);

            var response = await repository.SearchAsync(type: "", keyword: "", page: 1, limit: 20);

            Assert.True(response.Success);
            Assert.True(response.Result.Total > 0);
            Assert.NotEmpty(response.Result.Item);
        }
    }
}