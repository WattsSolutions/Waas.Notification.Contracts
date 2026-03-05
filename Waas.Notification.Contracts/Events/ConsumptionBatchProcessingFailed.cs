namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionBatchProcessingFailed : BaseEvent
    {
        public ConsumptionBatchProcessingFailed(
            string batchId,
            string reasonOfFailure)
        {
            BatchId = batchId;
            ReasonOfFailure = reasonOfFailure;
        }

        public string BatchId { get; }
        public string ReasonOfFailure { get; }
    }
}