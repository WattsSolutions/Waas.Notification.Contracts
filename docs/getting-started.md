# Getting Started with Waas.Notification.Contracts

This library provides the event contract types for **Watts-as-a-Service (WaaS)**. Use it to deserialize event data returned by the WaaS API into strongly-typed C# objects, or as a reference for the JSON shapes if you prefer to define your own models.

## Installation

Add the NuGet package to your project:

```bash
dotnet add package Waas.Notification.Contracts
```

The library targets **.NET Standard 2.1**, so it works with .NET Core 3.0+, .NET 5+, and any compatible runtime.

## Event Hierarchy

All events inherit from a common base. The hierarchy determines which properties are available:

```
BaseEvent                              (no properties)
├── DeviceEvent                        (+ DeviceId)
│   ├── AnalyticsSucceededEvent        (+ AnalyticsStart, AnalyticsEnd)
│   │   ├── HouseConsumptionForecastSucceeded
│   │   ├── HeatingConsumptionForecastSucceeded
│   │   ├── BaseConsumptionForecastSucceeded
│   │   └── ConsumptionDisaggregationSucceeded
│   ├── DeviceFailureEvent             (+ ReasonOfFailure)
│   │   ├── HouseConsumptionForecastFailed
│   │   ├── HeatingConsumptionForecastFailed
│   │   ├── BaseConsumptionForecastFailed
│   │   └── ConsumptionDisaggregationFailed
│   ├── DeviceProvisioned
│   ├── DeviceUnProvisioned
│   ├── ProvisionedDeviceUpdated
│   └── ConsumptionsAvailable
├── SpotPricesAvailable
├── SpotPriceForecastsAvailable
├── TransportationPricesAvailable
├── WeatherForecastsCollected
├── WeatherForecastsHistoryCollected
├── CO2AndOriginForecastsAvailable
├── ConsumptionBatchProcessingSucceeded
├── ConsumptionBatchProcessingFailed
├── ConsumptionBatchValidationFailed
└── ConsumptionBatchDeviceOnboardingsMissing
```

## Deserializing API Responses

When you poll the WaaS API, the response body contains JSON representing one of these event types. Deserialize it using `System.Text.Json`:

```csharp
using System.Text.Json;
using Waas.Notification.Contracts.Events;

// Example: deserializing a consumption event from an API response
var json = await httpClient.GetStringAsync("https://api.example.com/events/...");
var consumptions = JsonSerializer.Deserialize<ConsumptionsAvailable>(json);

Console.WriteLine($"Device: {consumptions.DeviceId}");
Console.WriteLine($"Last timestamp: {consumptions.LastTimestampInBatch}");
```

### Working with Device Events

Device-scoped events always include a `DeviceId`. Here's an example handling a device provisioning event:

```csharp
using Waas.Notification.Contracts.Events;
using Waas.Notification.Contracts.Enums;
using Waas.Notification.Contracts.Enums.HouseHoldConfiguration;

var json = """
{
  "DeviceId": "device-001",
  "Type": 1,
  "ProductionDeviceId": null,
  "Longitude": 12.5683,
  "Latitude": 55.6761,
  "PrimaryHeatingTypeValue": 3,
  "SecondaryHeatingTypeValue": 1,
  "HouseTypeValue": 2,
  "PersonCount": 4,
  "SizeInSquareMeters": 120,
  "ElectricVehiclesCount": 1,
  "StartDateTime": "2026-01-01T00:00:00",
  "EndDateTime": "2026-12-31T23:59:59"
}
""";

var device = JsonSerializer.Deserialize<DeviceProvisioned>(json);

// Enum values are stored as integers — cast to the enum for readability
var deviceType = (DeviceType)device.Type;                    // Electricity
var heatingType = (HeatingType)device.PrimaryHeatingTypeValue; // DistrictHeating
var houseType = (HouseType)device.HouseTypeValue;            // House

Console.WriteLine($"Device {device.DeviceId} is a {deviceType} meter");
Console.WriteLine($"Location: {device.Latitude}, {device.Longitude}");
Console.WriteLine($"House type: {houseType}, heated by: {heatingType}");
```

### Working with Analytics Events

Analytics events signal that a forecast computation has completed. Succeeded events include the time range that was analyzed:

```csharp
var json = """
{
  "DeviceId": "device-001",
  "AnalyticsStart": "2026-03-01T00:00:00",
  "AnalyticsEnd": "2026-03-23T00:00:00"
}
""";

var forecast = JsonSerializer.Deserialize<HouseConsumptionForecastSucceeded>(json);

Console.WriteLine($"Forecast for {forecast.DeviceId}");
Console.WriteLine($"Period: {forecast.AnalyticsStart:d} — {forecast.AnalyticsEnd:d}");
```

Failed analytics include a reason:

```csharp
var json = """
{
  "DeviceId": "device-001",
  "ReasonOfFailure": "Insufficient historical data for forecast model"
}
""";

var failure = JsonSerializer.Deserialize<HouseConsumptionForecastFailed>(json);

Console.WriteLine($"Forecast failed for {failure.DeviceId}: {failure.ReasonOfFailure}");
```

### Working with Market and Weather Events

These events are not device-scoped. They extend `BaseEvent` directly:

```csharp
var json = """
{
  "BiddingZone": "DK1",
  "From": "2026-03-23T00:00:00",
  "To": "2026-03-24T00:00:00"
}
""";

var spotPrices = JsonSerializer.Deserialize<SpotPricesAvailable>(json);

Console.WriteLine($"Spot prices for {spotPrices.BiddingZone}");
Console.WriteLine($"Period: {spotPrices.From:d} — {spotPrices.To:d}");
```

### Working with Batch Events

Batch events track bulk consumption data processing:

```csharp
var json = """
{
  "BatchId": "batch-2026-03-23-001"
}
""";

var batch = JsonSerializer.Deserialize<ConsumptionBatchProcessingSucceeded>(json);
Console.WriteLine($"Batch {batch.BatchId} processed successfully");
```

```csharp
var json = """
{
  "BatchId": "batch-2026-03-23-002",
  "ReasonOfFailure": "Invalid time series format in 3 records"
}
""";

var failure = JsonSerializer.Deserialize<ConsumptionBatchProcessingFailed>(json);
Console.WriteLine($"Batch {failure.BatchId} failed: {failure.ReasonOfFailure}");
```

## Bring Your Own Models

If you prefer not to take a dependency on this package, use the JSON shapes above as a reference and define your own types. The key things to know:

- All property names use **PascalCase** in JSON (default .NET serialization)
- Enum values are serialized as **integers**, not strings
- `DateTime` values use ISO 8601 format
- Nullable properties (e.g. `PersonCount`, `SizeInSquareMeters`, `ProductionDeviceId`) may be `null`

## Enum Reference

### DeviceType

| Value | Name |
|-------|------|
| 1 | Electricity |
| 2 | ElectricityProduction |

### TimeSeriesDuration

| Value | Name |
|-------|------|
| 1 | Hour |

### HeatingType

| Value | Name |
|-------|------|
| 0 | Unknown |
| 1 | WoodStove |
| 2 | ElectricalHeating |
| 3 | DistrictHeating |
| 4 | Gas |
| 5 | Oil |
| 6 | WoodPellets |
| 7 | HeatingPump |
| 8 | LocalDistribution |
| 9 | Geothermal |

### HouseType

| Value | Name |
|-------|------|
| 0 | Unknown |
| 1 | Apartment |
| 2 | House |
| 3 | HolidayHouse |
| 4 | TownHouse |
