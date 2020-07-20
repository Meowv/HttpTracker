using System;
using System.ComponentModel.DataAnnotations;

namespace HttpTracker
{
    public class PagingInput
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 限制条数
        /// </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 20;
    }
}