using Confluent.Kafka;

namespace Kafka.Producer.API.Interfaces
{
    public interface IKafkaProducer
    {
        Task<DeliveryResult<Null, string>> ProduceAsync(string topic, string message);
    }
}
