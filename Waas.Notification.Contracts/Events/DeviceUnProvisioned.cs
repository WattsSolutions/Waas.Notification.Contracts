namespace Waas.Notification.Contracts.Events
{
    public class DeviceUnProvisioned : DeviceEvent
    {
        public DeviceUnProvisioned(string deviceId) : base(deviceId)
        {
        }
    }
}