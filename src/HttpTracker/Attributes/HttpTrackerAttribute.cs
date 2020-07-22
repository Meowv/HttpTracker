using System;

namespace HttpTracker.Attributes
{
    /// <summary>
    /// HttpTracker Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpTrackerAttribute : Attribute
    {
        public HttpTrackerAttribute(string description)
        {
            Description = description;
        }

        public HttpTrackerAttribute(bool disabled)
        {
            Disabled = disabled;
        }

        public HttpTrackerAttribute(string description, bool disabled)
        {
            Disabled = disabled;
            Description = description;
        }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}