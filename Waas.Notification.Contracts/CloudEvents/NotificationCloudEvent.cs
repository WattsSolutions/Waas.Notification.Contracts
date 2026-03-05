using System;
using System.Text.Json.Serialization;
using Waas.Notification.Contracts.Constants;
using Waas.Notification.Contracts.Events;

namespace Waas.Notification.Contracts.CloudEvents
{
    public class NotificationCloudEvent<TEvent> where TEvent : BaseEvent
    {
        public NotificationCloudEvent(
            string id,
            string type,
            TEvent data,
            string trackingId,
            DateTimeOffset time)
        {
            Id = id;
            Type = type;
            Data = data;
            TrackingId = trackingId;
            Time = time;
        }

        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("time")]
        public DateTimeOffset Time { get; }

        [JsonPropertyName("data")]
        public TEvent Data { get; }

        [JsonPropertyName(CloudEventExtensionAttributeKeys.TrackingId)]
        public string TrackingId { get; }
    }
}