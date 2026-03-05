using System;

namespace Waas.Notification.Contracts.Events
{
    public class ProvisionedDeviceUpdated : DeviceEvent
    {
        public ProvisionedDeviceUpdated(
            string deviceId,
            string productionDeviceId,
            int primaryHeatingTypeValue,
            int secondaryHeatingTypeValue,
            int houseTypeValue,
            int? personCount,
            int? sizeInSquareMeters,
            int electricVehiclesCount,
            DateTime startDateTime,
            DateTime endDateTime
            ) : base(deviceId)
        {
            ProductionDeviceId = productionDeviceId;
            PrimaryHeatingTypeValue = primaryHeatingTypeValue;
            SecondaryHeatingTypeValue = secondaryHeatingTypeValue;
            HouseTypeValue = houseTypeValue;
            PersonCount = personCount;
            SizeInSquareMeters = sizeInSquareMeters;
            ElectricVehiclesCount = electricVehiclesCount;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public string? ProductionDeviceId { get; }
        /// <summary>Value of <see cref="Enums.HouseHoldConfiguration.HeatingType"/></summary>
        public int PrimaryHeatingTypeValue { get; }
        /// /// <summary>Value of <see cref="Enums.HouseHoldConfiguration.HeatingType"/></summary>
        public int SecondaryHeatingTypeValue { get; }
        /// /// <summary>Value of <see cref="Enums.HouseHoldConfiguration.HouseType"/></summary>
        public int HouseTypeValue { get; }
        public int? PersonCount { get; }
        public int? SizeInSquareMeters { get; }
        public int ElectricVehiclesCount { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
    }
}