namespace HttpTracker.Dto
{
    public class RequestInfo
    {
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
    }
}