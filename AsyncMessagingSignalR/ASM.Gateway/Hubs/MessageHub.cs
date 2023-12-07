using System.Text;
using System.Text.Json;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;

namespace ASM.Gateway.Hubs
{
    public class MessageHub : Hub
    {
        private IConnection _connection;

        public MessageHub(IConnection connection)
        {
            _connection = connection;
        }

        public Task SendMessage(string message)
        {
            var model = JsonSerializer.Serialize(new MessageModel{
                Message = message,
                ConnectionId = Context.ConnectionId,
            });

            using var channel = _connection.CreateModel();
            channel.BasicPublish(
                exchange: "",
                routingKey: "message",
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(model)
            );

            return Task.FromResult(1);
        }
    }
}