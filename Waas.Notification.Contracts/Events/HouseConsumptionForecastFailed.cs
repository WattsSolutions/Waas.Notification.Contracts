namespace Waas.Notification.Contracts.Events
{
    public class HouseConsumptionForecastFailed : DeviceFailureEvent
    {
        public HouseConsumptionForecastFailed(
            string deviceId,
            string reasonOfFailure) : base(deviceId, reasonOfFailure)
        {
        }
    }
}