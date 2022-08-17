using Ecommerce.MessageBus;
using Ecommerce.PaymentAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ecommerce.PaymentAPI.RabbitMqSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQMessageSender()
        {
            _hostName = "host.docker.internal";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            if(ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);

                channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
                channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);

                channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
                channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder");

                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: ExchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, "PaymentOrder", basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)message, options);
            return Encoding.UTF8.GetBytes(json);
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();

            }
            catch
            {
                throw;
            }
        }
        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }

    }
}
