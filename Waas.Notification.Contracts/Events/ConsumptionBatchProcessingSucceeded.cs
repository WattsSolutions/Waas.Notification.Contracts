namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionBatchProcessingSucceeded : BaseEvent
    {
        public ConsumptionBatchProcessingSucceeded(string batchId)
        {
            BatchId = batchId;
        }

        public string BatchId { get; }
    }
}