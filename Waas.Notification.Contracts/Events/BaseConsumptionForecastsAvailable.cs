namespace Waas.Notification.Contracts.Events
{
    public class BaseConsumptionForecastsAvailable : DeviceEvent
    {
        public BaseConsumptionForecastsAvailable(string deviceId) : base(deviceId)
        {
        }
    }
}