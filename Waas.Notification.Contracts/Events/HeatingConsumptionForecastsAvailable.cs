namespace Waas.Notification.Contracts.Events
{
    public class HeatingConsumptionForecastsAvailable : DeviceEvent
    {
        public HeatingConsumptionForecastsAvailable(string deviceId) : base(deviceId)
        {
        }
    }
}
