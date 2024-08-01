using System.Text;
using System.Text.Json;
using ClassRoom.Application.Services;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ClassRoom.Infrastracture
{
    public class MessageReceiver(IModel channel, ILogger<MessageReceiver> logger) : IMessageReceiver
    {
        public void Subscribe<TMessage>(string queueName, Func<TMessage, Task> handler)
        {
            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageStr = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<TMessage>(messageStr);

                logger.LogInformation("Message received: {message}", messageStr);

                if (message is null)
                {
                    logger.LogInformation("Message is null or empty: {message}", messageStr);
                    return;
                }

                try
                {
                    await handler(message);
                }
                catch (Exception e)
                {
                    logger.LogError("Message processing error: {message}. Error {error}", messageStr, e);
                    throw;
                }

                logger.LogInformation("Message processed: {message}", messageStr);

            };

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);

        }
    }
}
