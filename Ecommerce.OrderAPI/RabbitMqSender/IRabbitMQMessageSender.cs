using Ecommerce.MessageBus;

namespace Ecommerce.OrderAPI.RabbitMqSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
