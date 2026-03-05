namespace Waas.Notification.Contracts.Events
{
    public abstract class DeviceEvent : BaseEvent
    {
        protected DeviceEvent(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; }
    }
}