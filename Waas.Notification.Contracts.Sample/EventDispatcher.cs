using System.Text.Json;
using Waas.Notification.Contracts.CloudEvents;
using Waas.Notification.Contracts.Events;

namespace Waas.Notification.Contracts.Sample;

/// <summary>
/// Deserializes an incoming event envelope and dispatches to the correct handler.
/// Demonstrates the recommended pattern for consuming WaaS events.
/// </summary>
internal static class EventDispatcher
{
    public static void Dispatch(string eventType, string json)
    {
        switch (eventType)
        {
            // --- Device lifecycle ---
            case "waas.device.provisioned":
                Handle<DeviceProvisioned>(json, e =>
                    Console.WriteLine($"  Device provisioned → ID={e.DeviceId}  Type={e.Type}  EV count={e.ElectricVehiclesCount}"));
                break;

            case "waas.device.un-provisioned":
                Handle<DeviceUnProvisioned>(json, e =>
                    Console.WriteLine($"  Device removed → ID={e.DeviceId}"));
                break;

            case "waas.device.provisioned-device-updated":
                Handle<ProvisionedDeviceUpdated>(json, e =>
                    Console.WriteLine($"  Device updated → ID={e.DeviceId}  EV count={e.ElectricVehiclesCount}"));
                break;

            // --- Consumption data ---
            case "waas.device.consumptions-available":
                Handle<ConsumptionsAvailable>(json, e =>
                    Console.WriteLine($"  New consumption data → Device={e.DeviceId}  UpTo={e.LastTimestampInBatch:yyyy-MM-dd HH:mm}"));
                break;

            case "waas.batch.processing-succeeded":
                Handle<ConsumptionBatchProcessingSucceeded>(json, e =>
                    Console.WriteLine($"  Batch processed → BatchId={e.BatchId}"));
                break;

            case "waas.batch.processing-failed":
                Handle<ConsumptionBatchProcessingFailed>(json, e =>
                    Console.WriteLine($"  Batch failed → BatchId={e.BatchId}  Reason={e.ReasonOfFailure}"));
                break;

            // --- Analytics ---
            case "waas.analytics.house-consumption-forecast-succeeded":
                Handle<HouseConsumptionForecastSucceeded>(json, e =>
                    Console.WriteLine($"  House forecast ready → Device={e.DeviceId}  Period={e.AnalyticsStart:yyyy-MM-dd}→{e.AnalyticsEnd:yyyy-MM-dd}"));
                break;

            case "waas.analytics.house-consumption-forecast-failed":
                Handle<HouseConsumptionForecastFailed>(json, e =>
                    Console.WriteLine($"  House forecast failed → Device={e.DeviceId}  Reason={e.ReasonOfFailure}"));
                break;

            case "waas.analytics.consumption-disaggregation-succeeded":
                Handle<ConsumptionDisaggregationSucceeded>(json, e =>
                    Console.WriteLine($"  Disaggregation complete → Device={e.DeviceId}  Period={e.AnalyticsStart:yyyy-MM-dd}→{e.AnalyticsEnd:yyyy-MM-dd}"));
                break;

            case "waas.analytics.consumption-disaggregation-failed":
                Handle<ConsumptionDisaggregationFailed>(json, e =>
                    Console.WriteLine($"  Disaggregation failed → Device={e.DeviceId}  Reason={e.ReasonOfFailure}"));
                break;

            // --- Market ---
            case "waas.market.spot-prices-available":
                Handle<SpotPricesAvailable>(json, e =>
                    Console.WriteLine($"  Spot prices available → Zone={e.BiddingZone}  {e.From:yyyy-MM-dd}→{e.To:yyyy-MM-dd}"));
                break;

            case "waas.market.spot-price-forecasts-available":
                Handle<SpotPriceForecastsAvailable>(json, e =>
                    Console.WriteLine($"  Spot price forecasts available → Zone={e.BiddingZone}  {e.From:yyyy-MM-dd}→{e.To:yyyy-MM-dd}"));
                break;

            case "waas.market.transportation-prices-available":
                Handle<TransportationPricesAvailable>(json, e =>
                    Console.WriteLine($"  Transportation prices available → {e.CountryCode}/{e.GridAreaId}  {e.From:yyyy-MM-dd}→{e.To:yyyy-MM-dd}"));
                break;

            // --- Environment ---
            case "waas.environment.co2-and-origin-forecasts-available":
                Handle<CO2AndOriginForecastsAvailable>(json, e =>
                    Console.WriteLine($"  CO2/origin forecasts available → {e.From:yyyy-MM-dd}→{e.To:yyyy-MM-dd}"));
                break;

            case "waas.environment.weather-forecasts-collected":
                Handle<WeatherForecastsCollected>(json, e =>
                    Console.WriteLine($"  Weather forecasts collected → {e.From:yyyy-MM-dd}→{e.To:yyyy-MM-dd}"));
                break;

            default:
                Console.WriteLine($"  [unhandled event type: {eventType}]");
                break;
        }
    }

    private static void Handle<TEvent>(string json, Action<TEvent> handler) where TEvent : BaseEvent
    {
        var envelope = JsonSerializer.Deserialize<NotificationCloudEvent<TEvent>>(json)
            ?? throw new InvalidOperationException($"Failed to deserialize {typeof(TEvent).Name}");

        Console.WriteLine($"  trackingId: {envelope.TrackingId}");
        handler(envelope.Data);
    }
}
