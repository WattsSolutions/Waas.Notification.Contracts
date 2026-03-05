using System;
using Azure.Messaging;
using Waas.Notification.Contracts.Constants;

namespace Waas.Notification.Contracts.CloudEvents
{
    public static class CloudEventExtensions
    {
        public static string GetOrCreateTrackingId(this CloudEvent cloudEvent)
        {
            var trackingId = GetTrackingId(cloudEvent);
            return trackingId ?? GenerateTrackingId();
        }

        public static string? GetTrackingId(this CloudEvent cloudEvent) =>
            cloudEvent.GetExtensionAttributeValue<string?>(CloudEventExtensionAttributeKeys.TrackingId);

        public static void SetTrackingId(this CloudEvent cloudEvent, string trackingId)
        {
            if (string.IsNullOrWhiteSpace(trackingId))
                throw new ArgumentException($"{nameof(trackingId)} cannot be empty.");

            cloudEvent.ExtensionAttributes.Add(CloudEventExtensionAttributeKeys.TrackingId, trackingId);
        }

        private static string GenerateTrackingId() => Guid.NewGuid().ToString("D");

        private static TValue GetExtensionAttributeValue<TValue>(this CloudEvent cloudEvent, string key)
        {
            if (!cloudEvent.ExtensionAttributes.TryGetValue(key.ToLower(), out var propertyValue))
                return default!;

            return (TValue)propertyValue;
        }
    }
}