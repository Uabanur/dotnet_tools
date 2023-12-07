// See https://aka.ms/new-console-template for more information


using System.Text;
using System.Text.Json;
using Common.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

void log(string msg) => Console.WriteLine($" [*] {msg}");
void process(IModel model, MessageModel msg)
{
    log($"Accepted: {msg}");

    var response = new MessageModel {
        Message = $"{msg.Message} processed",
        ConnectionId = msg.ConnectionId,
    };

    model.BasicPublish(
        exchange: "",
        routingKey: "response",
        basicProperties: null,
        body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response))
    );

    log($"Completed: {msg}");
}

var factory = new ConnectionFactory {
    HostName = "rabbitmq"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "message",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);
channel.QueueDeclare(
    queue: "response",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    if (model is not EventingBasicConsumer ebc) 
        throw new ArgumentException("Expected model as EventingBasicConsumer");

    try 
    {
        var body = ea.Body.ToArray();
        var message = JsonSerializer.Deserialize<MessageModel>(Encoding.UTF8.GetString(body))!;
        log($"Received: {message}");
        process(ebc.Model, message);
        ebc.Model.BasicAck(ea.DeliveryTag, multiple: false);
    } catch (Exception e)
    {
        log($"Error: {e.Message}");
        ebc.Model.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
    }
};

channel.BasicConsume(
    queue: "message",
    autoAck: false,
    consumer: consumer
);

while (true) {
    Thread.Sleep(int.MaxValue);
}