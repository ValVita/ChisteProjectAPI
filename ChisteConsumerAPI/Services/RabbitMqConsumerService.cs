using System.Text;
using System.Text.Json;
using ChisteConsumerAPI.Data;
using ChisteConsumerAPI.Models;
using ChisteFetcherAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChisteConsumerAPI.Services
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _queueName = "jokes_queue";

        public RabbitMqConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                var joke = JsonSerializer.Deserialize<ChisteModel>(message);

                if (joke != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ChisteDbContext>();

                    var jokeEntity = new ChisteModelConsumer { ExternalId = joke.Id, Content = joke.Value };
                    dbContext.Jokes.Add(jokeEntity);
                    await dbContext.SaveChangesAsync();
                }
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
