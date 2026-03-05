using System;
using Waas.Notification.Contracts.Enums;
using Waas.Notification.Contracts.Enums.HouseHoldConfiguration;

namespace Waas.Notification.Contracts.Events
{
    public class DeviceProvisioned : DeviceEvent
    {
        public DeviceProvisioned(
            string deviceId,
            int type,
            string productionDeviceId,
            double longitude,
            double latitude,
            int primaryHeatingTypeValue,
            int secondaryHeatingTypeValue,
            int houseTypeValue,
            int? personCount,
            int? sizeInSquareMeters,
            int electricVehiclesCount,
            DateTime startDateTime,
            DateTime endDateTime)
            : base(deviceId)
        {
            Type = type;
            ProductionDeviceId = productionDeviceId;
            Longitude = longitude;
            Latitude = latitude;
            PrimaryHeatingTypeValue = primaryHeatingTypeValue;
            SecondaryHeatingTypeValue = secondaryHeatingTypeValue;
            HouseTypeValue = houseTypeValue;
            PersonCount = personCount;
            SizeInSquareMeters = sizeInSquareMeters;
            ElectricVehiclesCount = electricVehiclesCount;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        /// <summary>Value of <see cref="DeviceType"/></summary>
        public int Type { get; }
        public string? ProductionDeviceId { get; }
        public double Longitude { get; }
        public double Latitude { get; }
        /// <summary>Value of <see cref="HeatingType"/></summary>
        public int PrimaryHeatingTypeValue { get; }
        /// <summary>Value of <see cref="HeatingType"/></summary>
        public int SecondaryHeatingTypeValue { get; }
        /// <summary>Value of <see cref="HouseType"/></summary>
        public int HouseTypeValue { get; }
        public int? PersonCount { get; }
        public int? SizeInSquareMeters { get; }
        public int ElectricVehiclesCount { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
    }
}