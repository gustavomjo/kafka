using Confluent.Kafka;
using Kafka.Producer.API.Interfaces;

namespace Kafka.Producer.API
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BoostrapServer"]
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync(string topic, string message)
        {
            return await _producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = message
            });
        }
    }
}