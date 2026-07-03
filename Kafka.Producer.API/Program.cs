using Kafka.Producer.API;
using Kafka.Producer.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
builder.Services.AddScoped<ProducerService>();

var app = builder.Build();

app.MapPost("/", async (
    [FromServices] ProducerService service,
    [FromBody] PessoaModel pessoa) =>
{
    return await service.SendMessage(pessoa);
});

app.Run();