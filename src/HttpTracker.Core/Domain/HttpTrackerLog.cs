using System;

namespace HttpTracker.Domain
{
    public partial class HttpTrackerLog
    {
        /// <summary>
        /// 类型 <see cref="Types"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        #region Request

        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Referrer
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// IpAddress
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Milliseconds
        /// </summary>
        public int Milliseconds { get; set; }

        /// <summary>
        /// QueryString
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// Request Body
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// Cookies
        /// </summary>
        public string Cookies { get; set; }

        /// <summary>
        /// Headers
        /// </summary>
        public string Headers { get; set; }

        #endregion

        #region Response

        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Response Body
        /// </summary>
        public string ResponseBody { get; set; }

        #endregion

        #region Server

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

        #endregion

        #region Exception

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常的堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        #endregion

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}