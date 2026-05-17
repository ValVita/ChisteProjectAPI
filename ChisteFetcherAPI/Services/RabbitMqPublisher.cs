using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using ChisteFetcherAPI.Models;
using RabbitMQ.Client;
namespace ChisteFetcherAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqPublisher : IRabbitMqPublisher, IAsyncDisposable
     {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string _queueName = "jokes_queue";

        public RabbitMqPublisher()
        {
            var factory = new ConnectionFactory { HostName = "localhost", VirtualHost = "chistes",UserName = "valentina", Password = "valentina" };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(queue: _queueName, durable: true, exclusive: false, autoDelete: false).Wait();
        }

        public async Task PublishJokeAsync(ChisteModel joke)
        {
            var message = JsonSerializer.Serialize(joke);
            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);
        }

        public async ValueTask DisposeAsync()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}
