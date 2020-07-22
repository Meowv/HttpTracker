using Microsoft.Extensions.Options;

namespace HttpTracker.Options
{
    public class HttpTrackerSQLiteOptions : IOptions<HttpTrackerSQLiteOptions>
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; } = "meowv";

        public HttpTrackerSQLiteOptions Value => this;
    }
}