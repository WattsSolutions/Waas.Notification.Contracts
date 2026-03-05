using System;

namespace Waas.Notification.Contracts.Events
{
    public class TransportationPricesAvailable : BaseEvent
    {
        public TransportationPricesAvailable(string countryCode, string gridAreaId, DateTime from, DateTime to)
        {
            CountryCode = countryCode;
            GridAreaId = gridAreaId;
            From = from;
            To = to;
        }

        public string CountryCode { get; set; }
        public string GridAreaId { get; set; }

        public DateTime From { get; }
        public DateTime To { get; }
    }
}
