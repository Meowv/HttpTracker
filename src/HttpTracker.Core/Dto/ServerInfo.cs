namespace HttpTracker.Dto
{
    public class ServerInfo
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 进程Id
        /// </summary>
        public int? PId { get; set; }

        /// <summary>
        /// 主机地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}