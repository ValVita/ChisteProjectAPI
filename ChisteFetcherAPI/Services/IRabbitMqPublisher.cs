using ChisteFetcherAPI.Models;

namespace ChisteFetcherAPI.Services
{
    public interface IRabbitMqPublisher
    {
        Task PublishJokeAsync(ChisteModel joke);
    }
}
