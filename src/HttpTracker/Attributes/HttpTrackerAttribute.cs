using System;

namespace HttpTracker.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpTrackerAttribute : Attribute
    {
        public HttpTrackerAttribute()
        {
        }
    }
}