using System;

namespace Waas.Notification.Contracts.Events
{
    public class HeatingConsumptionForecastSucceeded : AnalyticsSucceededEvent
    {
        public HeatingConsumptionForecastSucceeded(
            string deviceId,
            DateTime analyticsStart,
            DateTime analyticsEnd) : base(deviceId, analyticsStart, analyticsEnd)
        {
        }
    }
}
