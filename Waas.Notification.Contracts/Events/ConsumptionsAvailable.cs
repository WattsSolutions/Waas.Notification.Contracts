using System;
using Waas.Notification.Contracts.Enums;

namespace Waas.Notification.Contracts.Events
{
    public class ConsumptionsAvailable : DeviceEvent
    {
        /// <param name="deviceId">Device ID</param>
        /// <param name="batchTimeSeriesDuration"> Batch time series duration enum. See <see cref="TimeSeriesDuration"/></param>
        /// <param name="lastTimestampInBatch">Timestamp of latest time series of imported batch.</param>
        public ConsumptionsAvailable(
            string deviceId,
            int batchTimeSeriesDuration,
            DateTime lastTimestampInBatch) : base(deviceId)
        {
            BatchTimeSeriesDuration = batchTimeSeriesDuration;
            LastTimestampInBatch = lastTimestampInBatch;
        }

        /// <summary>
        /// Batch time series duration enum. See <see cref="TimeSeriesDuration"/>
        /// </summary>
        public int BatchTimeSeriesDuration { get; }

        /// <summary>
        /// Timestamp of latest time series of imported batch.
        /// </summary>
        public DateTime LastTimestampInBatch { get; }
    }
}