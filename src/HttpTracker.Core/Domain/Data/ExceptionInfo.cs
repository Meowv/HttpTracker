namespace HttpTracker.Domain.Data
{
    public class ExceptionInfo
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误的堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }
    }
}