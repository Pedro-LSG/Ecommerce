using Ecommerce.OrderAPI.Messages;
using Ecommerce.OrderAPI.RabbitMqSender;
using Ecommerce.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Ecommerce.OrderAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPaymentConsumer(OrderRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
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
            _channel.QueueDeclare(queue: "orderpaymentresultqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultVO vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content) ?? new UpdatePaymentResultVO();
                UpdatePaymentStatus(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentresultqueue", false, consumer);
            var a = Task.CompletedTask;
            return a;
        }

        private async Task UpdatePaymentStatus(UpdatePaymentResultVO vo)
        {
            try
            {
                await _repository.UpdateOrderPaymentStatus(vo.OrderId, vo.Status);
            }
            catch
            {
                throw;
            }
        }
    }
}
