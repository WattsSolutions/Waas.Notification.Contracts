using System.Text.Json;
using Waas.Notification.Contracts.CloudEvents;
using Waas.Notification.Contracts.Events;

namespace Waas.Notification.Contracts.Sample;

/// <summary>
/// Produces sample JSON payloads that mimic what WaaS publishes on its event bus.
/// In production these arrive via Azure Service Bus / Event Grid.
/// </summary>
internal static class EventSimulator
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static IEnumerable<(string EventType, string Json)> GenerateSampleEvents()
    {
        yield return Wrap("waas.device.provisioned",
            new DeviceProvisioned(
                deviceId: "device-abc-123",
                type: 1,                        // DeviceType.Electricity
                productionDeviceId: "prod-xyz",
                longitude: 10.4515,
                latitude: 55.8657,
                primaryHeatingTypeValue: 1,     // HeatingType.HeatPump
                secondaryHeatingTypeValue: 0,   // HeatingType.None
                houseTypeValue: 1,              // HouseType.SingleFamily
                personCount: 3,
                sizeInSquareMeters: 140,
                electricVehiclesCount: 1,
                startDateTime: new DateTime(2024, 1, 1),
                endDateTime: new DateTime(9999, 12, 31)));

        yield return Wrap("waas.device.consumptions-available",
            new ConsumptionsAvailable(
                deviceId: "device-abc-123",
                batchTimeSeriesDuration: 1,     // TimeSeriesDuration.Hour
                lastTimestampInBatch: new DateTime(2024, 6, 15, 23, 0, 0)));

        yield return Wrap("waas.analytics.house-consumption-forecast-succeeded",
            new HouseConsumptionForecastSucceeded(
                deviceId: "device-abc-123",
                analyticsStart: new DateTime(2024, 6, 15),
                analyticsEnd: new DateTime(2024, 6, 16)));

        yield return Wrap("waas.analytics.consumption-disaggregation-succeeded",
            new ConsumptionDisaggregationSucceeded(
                deviceId: "device-abc-123",
                analyticsStart: new DateTime(2024, 6, 15),
                analyticsEnd: new DateTime(2024, 6, 16)));

        yield return Wrap("waas.market.spot-prices-available",
            new SpotPricesAvailable(
                biddingZone: "DK1",
                from: new DateTime(2024, 6, 16, 0, 0, 0),
                to: new DateTime(2024, 6, 17, 0, 0, 0)));

        yield return Wrap("waas.market.transportation-prices-available",
            new TransportationPricesAvailable(
                countryCode: "DK",
                gridAreaId: "DK1",
                from: new DateTime(2024, 6, 16, 0, 0, 0),
                to: new DateTime(2024, 6, 17, 0, 0, 0)));

        yield return Wrap("waas.environment.co2-and-origin-forecasts-available",
            new CO2AndOriginForecastsAvailable(
                from: new DateTime(2024, 6, 16, 0, 0, 0),
                to: new DateTime(2024, 6, 17, 0, 0, 0)));

        yield return Wrap("waas.analytics.house-consumption-forecast-failed",
            new HouseConsumptionForecastFailed(
                deviceId: "device-abc-123",
                reasonOfFailure: "Insufficient historical data"));
    }

    private static (string, string) Wrap<TEvent>(string type, TEvent data) where TEvent : BaseEvent
    {
        var envelope = new NotificationCloudEvent<TEvent>(
            id: Guid.NewGuid().ToString(),
            type: type,
            data: data,
            trackingId: Guid.NewGuid().ToString("D"),
            time: DateTimeOffset.UtcNow);

        return (type, JsonSerializer.Serialize(envelope, JsonOptions));
    }
}
