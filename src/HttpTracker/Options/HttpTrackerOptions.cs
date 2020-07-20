using Microsoft.Extensions.Options;

namespace HttpTracker.Options
{
    public class HttpTrackerOptions : IOptions<HttpTrackerOptions>
    {
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 监听服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 过滤请求
        /// </summary>
        public string[] FilterRequest { get; set; }

        public HttpTrackerOptions Value => this;
    }
}