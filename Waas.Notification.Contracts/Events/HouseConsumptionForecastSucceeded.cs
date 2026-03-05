using System;

namespace Waas.Notification.Contracts.Events
{
    public class HouseConsumptionForecastSucceeded : AnalyticsSucceededEvent
    {
        public HouseConsumptionForecastSucceeded(
            string deviceId,
            DateTime analyticsStart,
            DateTime analyticsEnd) : base(deviceId, analyticsStart, analyticsEnd)
        {
        }
    }
}