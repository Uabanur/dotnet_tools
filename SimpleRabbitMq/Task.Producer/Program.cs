using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory {
    HostName = Common.Constants.RabbitMqHost,
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(
    Common.Constants.TaskExchange,
    ExchangeType.Fanout,
    durable: false
);

var count = 0;
while(true)
{
    channel.BasicPublish(
        exchange: Common.Constants.TaskExchange,
        routingKey: "",
        basicProperties: null,
        body: Encoding.UTF8.GetBytes($"Task #{++count:D4}")
    );

    Thread.Sleep(TimeSpan.FromSeconds(1));
}