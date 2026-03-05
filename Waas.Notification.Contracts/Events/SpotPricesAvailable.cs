using System;

namespace Waas.Notification.Contracts.Events
{
    public class SpotPricesAvailable : BaseEvent
    {
        public SpotPricesAvailable(string biddingZone, DateTime from, DateTime to)
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
