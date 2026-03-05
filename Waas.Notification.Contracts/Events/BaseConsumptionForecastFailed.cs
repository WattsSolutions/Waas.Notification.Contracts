namespace Waas.Notification.Contracts.Events
{
    public class BaseConsumptionForecastFailed : DeviceFailureEvent
    {
        public BaseConsumptionForecastFailed(
            string deviceId,
            string reasonOfFailure) : base(deviceId, reasonOfFailure)
        {
        }
    }
}