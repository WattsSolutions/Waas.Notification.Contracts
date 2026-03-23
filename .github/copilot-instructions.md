# Copilot Instructions

## Overview

.NET Standard 2.1 class library defining public event contracts for Watts-as-a-Service (WaaS). Intended for NuGet distribution. The repo is the canonical reference for event schemas consumed by WaaS services.

## Commands

```bash
# Build
dotnet build

# Pack NuGet package
dotnet pack
```

There are no tests in this repository.

## Architecture

The library has three layers:

**Events** (`Events/`) — Domain event payloads in a multi-level inheritance hierarchy:

- `BaseEvent` → abstract root, no properties
  - `DeviceEvent` → abstract, adds `DeviceId`; base for all device-scoped events
    - `AnalyticsSucceededEvent` → abstract, adds `AnalyticsStart`/`AnalyticsEnd`; base for analytics result events
    - `DeviceFailureEvent` → abstract, adds `ReasonOfFailure`; base for analytics failure events
  - Non-device events (spot prices, weather, CO2) extend `BaseEvent` directly

**CloudEvents** (`CloudEvents/`) — Transport envelope:

- `NotificationCloudEvent<TEvent>` — generic typed wrapper with `id`, `type`, `time`, `data`, and `trackingid` extension attribute. Uses `[JsonPropertyName]` for lowercase JSON keys.
- `CloudEventExtensions` — extension methods on `Azure.Messaging.CloudEvent` for getting/setting/generating the `trackingid` extension attribute.

**Constants & Enums** — `CloudEventExtensionAttributeKeys` defines extension attribute names. Enums (`DeviceType`, `TimeSeriesDuration`, `HeatingType`, `HouseType`) live under `Enums/`.

## Key Conventions

- All new events **must** extend `BaseEvent` or an appropriate abstract subclass (`DeviceEvent`, `AnalyticsSucceededEvent`, `DeviceFailureEvent`).
- Events should be **immutable** — use read-only properties set via constructors.
- Enum values in event properties are stored as `int` (e.g., `int Type` for `DeviceType`), with XML doc comments referencing the enum type via `<see cref="..."/>`.
- CloudEvent extension attribute keys **must** be lowercase per the CloudEvents spec. Define them in `CloudEventExtensionAttributeKeys`.
- `NotificationCloudEvent<TEvent>` uses `System.Text.Json` with `[JsonPropertyName]` attributes — all properties on new events must be JSON-serializable.
- Domain event classes themselves do **not** use `[JsonPropertyName]` — they rely on default PascalCase serialization.
- Dependencies are intentionally minimal: only `Azure.Core` and `System.Text.Json`.
