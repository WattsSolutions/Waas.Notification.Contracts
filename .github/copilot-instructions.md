# Copilot Instructions

## Commands

```bash
dotnet build
dotnet pack
```

There are no tests in this repository.

## Architecture

This is a .NET Standard 2.1 class library defining public event contracts for Watts-as-a-Service (WaaS), distributed as a NuGet package.

### Event hierarchy

All events inherit from `BaseEvent`. Two abstract intermediaries shape the tree:

- **`DeviceEvent : BaseEvent`** — adds `DeviceId`; base for all device-scoped events
  - **`AnalyticsSucceededEvent : DeviceEvent`** — adds `AnalyticsStart`/`AnalyticsEnd`; base for all analytics result events (forecast succeeded/failed, disaggregation)
  - **`DeviceFailureEvent : DeviceEvent`** — adds `ReasonOfFailure`; base for device failure events
- Market/weather events (e.g., `SpotPricesAvailable`, `WeatherForecastsCollected`, `CO2AndOriginForecastsAvailable`, `TransportationPricesAvailable`) extend `BaseEvent` directly — they are not device-scoped

### CloudEvents layer (`CloudEvents/`)

- **`NotificationCloudEvent<TEvent>`** — typed transport envelope wrapping any `BaseEvent`. Carries `id`, `type`, `time`, `data`, and `trackingid`. Uses `System.Text.Json` with `[JsonPropertyName]` on all properties.
- **`CloudEventExtensions`** — extension methods on `Azure.Messaging.CloudEvent` to get, set, and auto-generate the `trackingid` extension attribute.

## Key Conventions

### Enum values are transported as `int`
Event constructors and properties accept enum-typed values as `int`, not the enum type itself. Annotate with an XML doc `<see cref="EnumType"/>` to indicate which enum to cast to:

```csharp
/// <summary>Value of <see cref="DeviceType"/></summary>
public int Type { get; }
```

This applies to `DeviceType`, `TimeSeriesDuration`, `HeatingType`, and `HouseType`.

### Properties are immutable
All event properties are get-only and set exclusively via the constructor. Do not add settable properties.

### CloudEvent extension attribute keys must be lowercase
The CloudEvents spec requires lowercase extension attribute names. Constants are defined in `CloudEventExtensionAttributeKeys` (e.g., `"trackingid"`, `"tenantid"`). The `GetExtensionAttributeValue` helper calls `.ToLower()` on the key for safety — keep all constants lowercase at the source.

### New events
- Extend `BaseEvent`, `DeviceEvent`, or `AnalyticsSucceededEvent` as appropriate.
- Device-scoped → `DeviceEvent`. Analytics result → `AnalyticsSucceededEvent`. Non-device → `BaseEvent`.
- Keep all properties JSON-serializable (required for `NotificationCloudEvent<TEvent>` serialization).
