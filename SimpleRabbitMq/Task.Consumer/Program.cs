using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory {
    HostName = "rabbitmq",
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Ready to receive messages!");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [*] Received {message}");
};

channel.BasicConsume(
    queue: "hello",
    autoAck: true,
    consumer: consumer
);

Console.WriteLine("Going to sleep");
while(true)
{
    Thread.Sleep(int.MaxValue);
}