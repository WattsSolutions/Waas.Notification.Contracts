using System;

namespace Waas.Notification.Contracts.Events
{
    public class WeatherForecastsCollected : BaseEvent
    {
        public WeatherForecastsCollected(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }
    }
}
