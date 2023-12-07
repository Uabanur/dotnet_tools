using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory {
    HostName = "rabbitmq"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

const string message = "Hello, world!";
var body = Encoding.UTF8.GetBytes(message);



while(true)
{
    channel.BasicPublish(
        exchange: "",
        routingKey: "hello",
        basicProperties: null,
        body: body
    );

    // Thread.Sleep(TimeSpan.FromMilliseconds(1));
}