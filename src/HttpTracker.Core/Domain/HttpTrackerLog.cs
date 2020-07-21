using HttpTracker.Domain.Data;
using System;

namespace HttpTracker.Domain
{
    public partial class HttpTrackerLog : IData
    {
        public HttpTrackerLog()
        {
            CreationTime = DateTimeOffset.UtcNow;

            Data = new DataDictionary();
        }

        /// <summary>
        /// 年月
        /// </summary>
        public string YearMonth { get; set; }

        /// <summary>
        /// 类型 <see cref="Types"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 附加数据 <see cref="DataKeys"/>
        /// </summary>
        public DataDictionary Data { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreationTime { get; set; }
    }
}