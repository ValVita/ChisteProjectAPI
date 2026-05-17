using System.Diagnostics.CodeAnalysis;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChisteConsumerAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IJokePersistenceService _jokePersistenceService;
        private readonly string _queueName = "jokes_queue";

        public RabbitMqConsumerService(IJokePersistenceService jokePersistenceService)
        {
            _jokePersistenceService = jokePersistenceService;
            var factory = new ConnectionFactory { HostName = "localhost", VirtualHost = "chistes", UserName = "valentina", Password = "valentina"};
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(queue: _queueName, durable: true, exclusive: false, autoDelete: false).Wait();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await _jokePersistenceService.TrySaveJokeFromJsonAsync(message);
                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await _channel.BasicConsumeAsync(_queueName, autoAck: false, consumer: consumer);
            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
            await base.StopAsync(cancellationToken);
        }
    }
}
