using System.Text.Json;
using ChisteConsumerAPI.Data;
using ChisteConsumerAPI.Models;
using ChisteFetcherAPI.Models;

namespace ChisteConsumerAPI.Services
{
    public class JokePersistenceService : IJokePersistenceService
    {
        private readonly IServiceProvider _serviceProvider;

        public JokePersistenceService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> TrySaveJokeFromJsonAsync(string message, CancellationToken cancellationToken = default)
        {
            ChisteModelC? joke;
            try
            {
                joke = JsonSerializer.Deserialize<ChisteModelC>(message);
            }
            catch (JsonException)
            {
                return false;
            }

            if (joke == null)
            {
                return false;
            }

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ChisteDbContext>();

            var jokeEntity = new ChisteModelConsumer { ExternalId = joke.Id, Content = joke.Value };
            dbContext.CHISTEDB.Add(jokeEntity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
