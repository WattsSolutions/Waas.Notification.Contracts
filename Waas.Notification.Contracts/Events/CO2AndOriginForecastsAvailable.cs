using System;

namespace Waas.Notification.Contracts.Events
{
    public class CO2AndOriginForecastsAvailable: BaseEvent
    {
        public CO2AndOriginForecastsAvailable(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime To { get; set; }

        public DateTime From { get; set; }
    }
}