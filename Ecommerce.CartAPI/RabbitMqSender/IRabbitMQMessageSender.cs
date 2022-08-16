using Ecommerce.MessageBus;

namespace Ecommerce.CartAPI.RabbitMqSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
