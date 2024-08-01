namespace ClassRoom.Application.Services
{
    public interface IMessageReceiver
    {
        public void Subscribe<TMessage>(string queueName, Func<TMessage, Task> handler);
    }
}
