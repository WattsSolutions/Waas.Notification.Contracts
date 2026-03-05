namespace Waas.Notification.Contracts.Events
{
    public class HeatingConsumptionEstimatesAvailable : DeviceEvent
    {
        public HeatingConsumptionEstimatesAvailable(string deviceId) : base(deviceId)
        {
        }
    }
}