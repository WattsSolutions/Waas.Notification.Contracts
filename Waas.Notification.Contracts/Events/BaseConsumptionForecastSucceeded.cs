using System;

namespace Waas.Notification.Contracts.Events
{
    public class BaseConsumptionForecastSucceeded : AnalyticsSucceededEvent
    {
        public BaseConsumptionForecastSucceeded(
            string deviceId,
            DateTime analyticsStart,
            DateTime analyticsEnd) : base(deviceId, analyticsStart, analyticsEnd)
        {
        }
    }
}