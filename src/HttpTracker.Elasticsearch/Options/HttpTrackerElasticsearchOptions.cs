using Microsoft.Extensions.Options;

namespace HttpTracker.Options
{
    public class HttpTrackerElasticsearchOptions : IOptions<HttpTrackerElasticsearchOptions>
    {
        /// <summary>
        /// 节点
        /// </summary>
        public string[] Nodes { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 索引前缀
        /// </summary>
        public string IndexPrefix { get; set; } = "meowv";

        public HttpTrackerElasticsearchOptions Value => this;
    }
}