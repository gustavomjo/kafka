using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kafka.Consumer.Console
{
    internal class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;
        private readonly ILogger<ConsumerService> _logger;
        private readonly ParametersModel _params;
        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
            _params = new ParametersModel();
            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _params.BoostrapServer,
                GroupId = _params.GroupId,
                //AutoOffsetReset = AutoOffsetReset.Earliest -> quando pegar a mensagem marcar como lida para nenhum consumidor possa pega-la novamente.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando mensagem...");
            //subscribe é a inscrição no topico que queremos consumir, ele que informa se tem alguma nova mensagem
            _consumer.Subscribe(_params.TopicName);

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);

                var pessoa = JsonSerializer.Deserialize<PessoaModel>(result.Message.Value);

                _logger.LogInformation($"GroupId: {_params.GroupId} Mensagem: {result.Message.Value}");
            }

        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _consumer.Close();
            _logger.LogInformation("Aplicação parou, conexão fechada");
            return Task.CompletedTask;
        }
    }
}
