
using System.Text;
using System.Text.Json;
using ASM.Gateway.Hubs;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ASM.Gateway.Workers;

public class MessageResponseWorker : BackgroundService
{
    private readonly ILogger<MessageResponseWorker> _logger;
    private readonly IConnection _rabbitmqConnection;
    private readonly IHubContext<MessageHub> _hub;

    public MessageResponseWorker(
        ILogger<MessageResponseWorker> logger, 
        IConnection rabbitmqConnection, 
        IHubContext<MessageHub> hub)
    {
        _logger = logger;
        _rabbitmqConnection = rabbitmqConnection;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var channel = _rabbitmqConnection.CreateModel();
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            _logger.LogInformation("Received message response");
            if (model is not EventingBasicConsumer ebc) 
                throw new ArgumentException("Expected model as EventingBasicConsumer");

            try 
            {
                var message = JsonSerializer.Deserialize<MessageModel>(Encoding.UTF8.GetString(ea.Body.ToArray()))!;
                var client = _hub.Clients.Client(message.ConnectionId);
                await client.SendAsync("MessageResponse", message.Message, CancellationToken.None);
                ebc.Model.BasicAck(ea.DeliveryTag, multiple: false);
            } catch (Exception e)
            {
                _logger.LogError(e, "Failed to send message response.");
                ebc.Model.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        channel.BasicConsume(
            queue: "response",
            autoAck: false,
            consumer: consumer
        );

        _logger.LogInformation("Registered consumer");
        
        while(true)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}