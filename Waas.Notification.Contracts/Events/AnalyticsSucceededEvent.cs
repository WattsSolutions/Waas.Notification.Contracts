using System;

namespace Waas.Notification.Contracts.Events
{
    public abstract class AnalyticsSucceededEvent : DeviceEvent
    {
        protected AnalyticsSucceededEvent(
            string deviceId,
            DateTime analyticsStart,
            DateTime analyticsEnd) : base(deviceId)
        {
            AnalyticsStart = analyticsStart;
            AnalyticsEnd = analyticsEnd;
        }

        public DateTime AnalyticsStart { get; }
        public DateTime AnalyticsEnd { get; }
    }
}
