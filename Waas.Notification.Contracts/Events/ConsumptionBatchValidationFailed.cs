using System.Collections.Generic;

namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionBatchValidationFailed : BaseEvent
    {
        public ConsumptionBatchValidationFailed(
            string batchId,
            IDictionary<string, IEnumerable<string>> errors)
        {
            BatchId = batchId;
            Errors = errors;
        }

        public string BatchId { get; }
        public IDictionary<string, IEnumerable<string>> Errors { get; }
    }
}