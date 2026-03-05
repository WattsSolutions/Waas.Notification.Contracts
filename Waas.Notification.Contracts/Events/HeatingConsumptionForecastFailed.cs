namespace Waas.Notification.Contracts.Events
{
    public class HeatingConsumptionForecastFailed : DeviceFailureEvent
    {
        public HeatingConsumptionForecastFailed(
            string deviceId,
            string reasonOfFailure) : base(deviceId, reasonOfFailure)
        {
        }
    }
}
