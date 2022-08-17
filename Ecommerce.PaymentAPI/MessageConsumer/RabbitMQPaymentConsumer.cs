using Ecommerce.PaymentAPI.Messages;
using Ecommerce.PaymentAPI.RabbitMqSender;
using Ecommerce.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Ecommerce.PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _processPayment = processPayment ?? throw new ArgumentNullException(nameof(processPayment));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));

            var factory = new ConnectionFactory()
            {
                HostName = "host.docker.internal",
                Password = "guest",
                UserName = "guest"
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content) ?? new PaymentMessage();
                ProcessPayment(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            var a = Task.CompletedTask;
            return a;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {
            var result = _processPayment.PaymentProcessor(); //Representa o serviço de pagamento mas não será implementado (retorna sempre true)

            UpdatePaymentResultMessage paymentResult = new()
            {
                Status = result,
                OrderId = vo.OrderId,
                Email = vo.Email
            };

            ProcessPayment(paymentResult);
        }

        private void ProcessPayment(UpdatePaymentResultMessage paymentResultMessage)
        {
            try
            {
                _rabbitMQMessageSender.SendMessage(paymentResultMessage, "orderpaymentresultqueue");
            }
            catch
            {
                throw;
            }
        }
    }
}
