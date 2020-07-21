using Microsoft.Extensions.Options;

namespace HttpTracker.Options
{
    public class HttpTrackerMongoDbOptions : IOptions<HttpTrackerMongoDbOptions>
    {
        /// <summary>
        /// 服务列表
        /// </summary>
        public string[] Services { get; set; }

        /// <summary>
        /// 连接模式
        /// </summary>
        public string ConnectionMode { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 集合前缀
        /// </summary>
        public string CollectionPrefix { get; set; } = "meowv";

        public HttpTrackerMongoDbOptions Value => this;
    }
}