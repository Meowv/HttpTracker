using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpTracker.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取UserAgent
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpContext context)
        {
            return context.Request.Headers["user-agent"].FirstOrDefault();
        }

        /// <summary>
        /// 获取HttpMethod
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetHttpMethod(this HttpContext context)
        {
            return context.Request.Method;
        }

        /// <summary>
        /// 获取URI
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpContext context)
        {
            return new StringBuilder().Append(context.Request.Scheme)
                                      .Append("://")
                                      .Append(context.Request.Host)
                                      .Append(context.Request.PathBase)
                                      .Append(context.Request.Path)
                                      .Append(context.Request.QueryString)
                                      .ToString();
        }

        /// <summary>
        /// 获取Referer
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetReferer(this HttpContext context)
        {
            return context.Request.Headers["referer"].FirstOrDefault();
        }

        /// <summary>
        /// 获取Ip地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetIpAddress(this HttpContext context)
        {
            var ip = context.Request.Headers["x-forwarded-for"].FirstOrDefault() ??
                     context.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return ip;
        }

        /// <summary>
        /// 获取QueryString
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetQueryString(this HttpContext context)
        {
            return context.Request.QueryString.Value;
        }

        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<string> GetRequestBodyAsync(this HttpContext context)
        {
            try
            {
                string result = string.Empty;

                context.Request.EnableBuffering();

                var requestReader = new StreamReader(context.Request.Body, Encoding.UTF8);

                result = await requestReader.ReadToEndAsync();

                context.Request.Body.Position = 0;

                return HttpUtility.HtmlDecode(result);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetCookies(this HttpContext context)
        {
            return JsonConvert.SerializeObject(context.Request.Cookies);
        }

        /// <summary>
        /// 获取Headers
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetHeaders(this HttpContext context)
        {
            return JsonConvert.SerializeObject(context.Request.Headers);
        }

        /// <summary>
        /// 获取状态码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetStatusCode(this HttpContext context)
        {
            return context.Response.StatusCode;
        }

        /// <summary>
        /// 获取返回数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<string> GetResponseBodyAsync(this HttpContext context)
        {
            try
            {
                string result = string.Empty;

                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var responseReader = new StreamReader(context.Response.Body, Encoding.UTF8);

                result = await responseReader.ReadToEndAsync();

                context.Response.Body.Seek(0, SeekOrigin.Begin);

                return HttpUtility.HtmlDecode(result);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}