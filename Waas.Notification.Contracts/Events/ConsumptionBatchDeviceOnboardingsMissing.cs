using System.Collections.Generic;

namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionBatchDeviceOnboardingsMissing : BaseEvent
    {
        public ConsumptionBatchDeviceOnboardingsMissing(
            string batchId,
            IEnumerable<string> notOnboardedDeviceIds)
        {
            BatchId = batchId;
            NotOnboardedDeviceIds = notOnboardedDeviceIds;
        }

        public string BatchId { get; }
        public IEnumerable<string> NotOnboardedDeviceIds { get; }
    }
}