using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var consumerId = Guid.NewGuid().ToString().Substring(0, 5);

void log(string msg) => Console.WriteLine($" [{consumerId}] {msg}");
void process(string msg) => log($"Logged: {msg}");

var factory = new ConnectionFactory {
    HostName = Common.Constants.RabbitMqHost,
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(
    Common.Constants.LogExchange,
    ExchangeType.Fanout,
    durable: false
);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName, 
    exchange: Common.Constants.LogExchange,
    routingKey: ""
);

log("Ready to receive messages!");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    if (model is not EventingBasicConsumer ebc) 
        throw new ArgumentException("Expected model as EventingBasicConsumer");

    try 
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        log($"Received: {message}");
        process(message);
        ebc.Model.BasicAck(ea.DeliveryTag, multiple: false);
    } catch (Exception e)
    {
        log($"Error: {e.Message}");
        ebc.Model.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
    }
};

channel.BasicConsume(
    queue: queueName,
    consumerTag: consumerId,
    autoAck: false,
    consumer: consumer
);

log("Going to sleep");
while(true)
{
    Thread.Sleep(int.MaxValue);
}