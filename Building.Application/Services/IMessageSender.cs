namespace Building.Application.Services;

public interface IMessageSender
{
    void SendMessage<T>(string queueName, T message);
}