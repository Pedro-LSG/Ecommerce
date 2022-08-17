using Ecommerce.MessageBus;

namespace Ecommerce.PaymentAPI.RabbitMqSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
