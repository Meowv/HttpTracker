namespace HttpTracker.Dto
{
    public class ExceptionInfo
    {
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
    }
}