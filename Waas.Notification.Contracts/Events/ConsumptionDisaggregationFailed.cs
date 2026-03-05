namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionDisaggregationFailed : DeviceFailureEvent
    {
        public ConsumptionDisaggregationFailed(
            string deviceId,
            string reasonOfFailure) : base(deviceId, reasonOfFailure)
        {
        }
    }
}
