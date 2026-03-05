namespace Waas.Notification.Contracts.Events
{
    public abstract class DeviceFailureEvent : DeviceEvent
    {
        protected DeviceFailureEvent(
            string deviceId,
            string reasonOfFailure) : base(deviceId)
        {
            ReasonOfFailure = reasonOfFailure;
        }

        public string ReasonOfFailure { get; }
    }
}
