using HttpTracker.Domain.Data;

namespace HttpTracker.Dto
{
    public class HttpTrackerLogDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 请求信息
        /// </summary>
        public RequestInfo Request { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public ResponseInfo Response { get; set; }

        /// <summary>
        /// 服务信息
        /// </summary>
        public ServerInfo Server { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public ExceptionInfo Exception { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
    }
}