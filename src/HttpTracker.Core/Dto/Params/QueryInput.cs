using System;
using System.ComponentModel.DataAnnotations;

namespace HttpTracker.Dto.Params
{
    public class QueryInput
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// 限制条数
        /// </summary>
        [Range(1, 100)]
        public int Limit { get; set; } = 20;
    }
}