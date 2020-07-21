using System;

namespace HttpTracker
{
    public static class HttpTrackerInstance
    {
        public static string InstanceName => DateTime.Now.ToString("yyyyMM");
    }
}