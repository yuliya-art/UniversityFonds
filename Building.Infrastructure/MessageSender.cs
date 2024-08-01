using System.Text;
using System.Text.Json;
using Building.Application.Services;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Building.Infrastructure
{
    public class MessageSender(ConnectionFactory factory, ILogger<MessageSender> logger) : IMessageSender
    {
        public void SendMessage<T>(string queueName, T message)
        {
            var messageStr = JsonSerializer.Serialize(message);
            logger.LogInformation("Message sending started: {message}", messageStr);

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

          
            var body = Encoding.UTF8.GetBytes(messageStr);

            channel.BasicPublish(exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: body);

            logger.LogInformation("Message is sent: {message}", messageStr);
        }
    }
}
