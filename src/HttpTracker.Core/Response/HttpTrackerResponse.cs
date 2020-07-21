using System;

namespace HttpTracker.Response
{
    public class HttpTrackerResponse
    {
        public bool Success = true;

        public string Message { get; set; }

        public long Timestamp { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        public void IsSuccess(string message = "")
        {
            Message = message;
        }

        public void IsFailed(string message = "")
        {
            Message = message;
            Success = !Success;
        }

        public void IsFailed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Success = !Success;
        }
    }
}