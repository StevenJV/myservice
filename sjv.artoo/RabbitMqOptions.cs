using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sjv.artoo
{
    public class RabbitMqOptions
    {
        public string AmqpUri { get; set; }
        public int RetryCount { get; set; }
        public int WaitIntervalSeconds { get; set; }

        public Uri SendAddress(string queueName)
        {
            return new Uri($"{AmqpUri}{queueName}");
        }
    }
}
