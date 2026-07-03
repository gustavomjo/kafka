using Confluent.Kafka;
using Kafka.Producer.API.Interfaces;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Producer.API
{
    public class ProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProducerService> _logger;
        private readonly IKafkaProducer _producer;
        public ProducerService(IConfiguration configuration, ILogger<ProducerService> logger, IKafkaProducer producer )
        {
            _configuration = configuration;
            _logger = logger;
            var boostrap = _configuration.GetSection("KafkaConfig").GetSection("BoostrapServer").Value;
            _logger = logger;
            _producer = producer;
        }

        public async Task<string> SendMessage(PessoaModel pessoa) {
            var topico = _configuration.GetSection("KafkaConfig").GetSection("TopicName").Value;
            try
            {
                var message = JsonSerializer.Serialize(pessoa);
                var result = await _producer.ProduceAsync(topico, message);
                _logger.LogInformation(result.Status.ToString());
                return $"{result.Status} - {message}";
            }
            catch
            {
                _logger.LogError("Erro ao enviar mensagem");
                return ("Erro ao enviar mensagem");
            }
        }
    }
}
