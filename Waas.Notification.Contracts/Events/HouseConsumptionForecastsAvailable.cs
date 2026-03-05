namespace Waas.Notification.Contracts.Events
{
    public class HouseConsumptionForecastsAvailable : DeviceEvent
    {
        public HouseConsumptionForecastsAvailable(string deviceId) : base(deviceId)
        {
        }
    }
}