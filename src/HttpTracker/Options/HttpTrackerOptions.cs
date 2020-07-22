using Microsoft.Extensions.Options;

namespace HttpTracker.Options
{
    public class HttpTrackerOptions : IOptions<HttpTrackerOptions>
    {
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; } = "api_meowv";

        /// <summary>
        /// 服务主机
        /// </summary>
        public string ServerHost { get; set; } = "132.232.126.92";

        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServerPort { get; set; } = 5000;

        /// <summary>
        /// 过滤请求
        /// </summary>
        public string[] FilterRequest { get; set; } = { "/swagger" };

        public HttpTrackerOptions Value => this;
    }
}