namespace HttpTracker.Response
{
    public class HttpTrackerResponse<TResult> : HttpTrackerResponse where TResult : class
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public TResult Result { get; set; }

        public void IsSuccess(TResult result = null, string message = "")
        {
            Message = message;
            Result = result;
        }
    }
}