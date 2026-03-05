using System;

namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionDisaggregationSucceeded : AnalyticsSucceededEvent
    {
        public ConsumptionDisaggregationSucceeded(
            string deviceId,
            DateTime analyticsStart,
            DateTime analyticsEnd) : base(deviceId, analyticsStart, analyticsEnd)
        {
        }
    }
}
