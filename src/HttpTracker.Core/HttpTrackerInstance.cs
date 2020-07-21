using System;

namespace HttpTracker
{
    public static class HttpTrackerInstance
    {
        public static string InstanceName => DateTimeOffset.UtcNow.LocalDateTime.ToString("yyyyMM");
    }
}