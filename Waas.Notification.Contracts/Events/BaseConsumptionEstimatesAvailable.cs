namespace Waas.Notification.Contracts.Events
{
    public class BaseConsumptionEstimatesAvailable : DeviceEvent
    {
        public BaseConsumptionEstimatesAvailable(string deviceId) : base(deviceId)
        {
        }
    }
}