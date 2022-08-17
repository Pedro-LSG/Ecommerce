using Ecommerce.Email.Messages;
using Ecommerce.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Email.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly EmailRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";

        public RabbitMQPaymentConsumer(EmailRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            var factory = new ConnectionFactory()
            {
                HostName = "host.docker.internal",
                Password = "guest",
                UserName = "guest"
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
            _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultMessage message = JsonSerializer.Deserialize<UpdatePaymentResultMessage>(content) ?? new UpdatePaymentResultMessage();
                ProcessLogs(message).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(PaymentEmailUpdateQueueName, false, consumer);
            var a = Task.CompletedTask;
            return a;
        }

        private async Task ProcessLogs(UpdatePaymentResultMessage message)
        {
            try
            {
                await _repository.LogEmail(message);
            }
            catch
            {
                throw;
            }
        }
    }
}
