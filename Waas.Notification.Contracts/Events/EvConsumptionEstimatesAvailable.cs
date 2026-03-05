namespace Waas.Notification.Contracts.Events
{
    public class EvConsumptionEstimatesAvailable : DeviceEvent
    {
        public EvConsumptionEstimatesAvailable(string deviceId) : base(deviceId)
        {
        }
    }
}