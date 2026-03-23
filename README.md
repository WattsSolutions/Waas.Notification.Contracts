# Waas.Notification.Contracts

Contracts for Watts-as-a-Service (WaaS) public events.

## Status
- Intended for NuGet distribution, but the feed is not public right now (as of 2026-03).
- Use this repository as a reference for event schemas.

## Event hierarchy

```mermaid
classDiagram
    class BaseEvent
    class DeviceEvent {
        +string DeviceId
    }
    class AnalyticsSucceededEvent {
        +DateTime AnalyticsStart
        +DateTime AnalyticsEnd
    }
    class DeviceFailureEvent {
        +string ReasonOfFailure
    }

    BaseEvent <|-- DeviceEvent
    BaseEvent <|-- SpotPricesAvailable
    BaseEvent <|-- SpotPriceForecastsAvailable
    BaseEvent <|-- WeatherForecastsCollected
    BaseEvent <|-- WeatherForecastsHistoryCollected
    BaseEvent <|-- TransportationPricesAvailable
    BaseEvent <|-- CO2AndOriginForecastsAvailable
    BaseEvent <|-- ConsumptionBatchProcessingSucceeded
    BaseEvent <|-- ConsumptionBatchProcessingFailed
    BaseEvent <|-- ConsumptionBatchValidationFailed
    BaseEvent <|-- ConsumptionBatchDeviceOnboardingsMissing

    DeviceEvent <|-- ConsumptionsAvailable
    DeviceEvent <|-- DeviceProvisioned
    DeviceEvent <|-- DeviceUnProvisioned
    DeviceEvent <|-- ProvisionedDeviceUpdated
    DeviceEvent <|-- AnalyticsSucceededEvent
    DeviceEvent <|-- DeviceFailureEvent

    AnalyticsSucceededEvent <|-- HouseConsumptionForecastSucceeded
    AnalyticsSucceededEvent <|-- HouseConsumptionForecastFailed
    AnalyticsSucceededEvent <|-- HeatingConsumptionForecastSucceeded
    AnalyticsSucceededEvent <|-- HeatingConsumptionForecastFailed
    AnalyticsSucceededEvent <|-- BaseConsumptionForecastSucceeded
    AnalyticsSucceededEvent <|-- BaseConsumptionForecastFailed
    AnalyticsSucceededEvent <|-- ConsumptionDisaggregationSucceeded
    AnalyticsSucceededEvent <|-- ConsumptionDisaggregationFailed
    AnalyticsSucceededEvent <|-- HouseConsumptionForecastsAvailable
    AnalyticsSucceededEvent <|-- HeatingConsumptionForecastsAvailable
    AnalyticsSucceededEvent <|-- BaseConsumptionForecastsAvailable
    AnalyticsSucceededEvent <|-- BaseConsumptionEstimatesAvailable
    AnalyticsSucceededEvent <|-- EvConsumptionEstimatesAvailable
    AnalyticsSucceededEvent <|-- HeatingConsumptionEstimatesAvailable
```

## Quick start

Until the NuGet feed is public, clone this repo and add a project reference:

```xml
<ProjectReference Include="../Waas.Notification.Contracts/Waas.Notification.Contracts.csproj" />
```

Or build a local package and reference it from a local NuGet source:

```bash
dotnet pack
```

### Publishing an event

```csharp
using Waas.Notification.Contracts.CloudEvents;
using Waas.Notification.Contracts.Events;

var cloudEvent = new NotificationCloudEvent<SpotPricesAvailable>(
    id: Guid.NewGuid().ToString(),
    type: "waas.market.spot-prices-available",
    data: new SpotPricesAvailable("DK1", DateTime.UtcNow, DateTime.UtcNow.AddHours(24)),
    trackingId: Guid.NewGuid().ToString("D"),
    time: DateTimeOffset.UtcNow);

var json = JsonSerializer.Serialize(cloudEvent);
```

### Consuming an event via Azure.Messaging.CloudEvent

```csharp
using Azure.Messaging;
using Waas.Notification.Contracts.CloudEvents;

// On receive:
string trackingId = incomingCloudEvent.GetOrCreateTrackingId();
var data = incomingCloudEvent.Data.ToObjectFromJson<SpotPricesAvailable>();
```

