using System;

namespace Waas.Notification.Contracts.Events
{
    public class SpotPriceForecastsAvailable : BaseEvent
    {
        public SpotPriceForecastsAvailable(string biddingZone, DateTime from, DateTime to)
        {
            BiddingZone = biddingZone;
            From = from;
            To = to;
        }

        public string BiddingZone { get; }
        public DateTime From { get; }
        public DateTime To { get; }
    }
}
