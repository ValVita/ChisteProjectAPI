namespace ChisteConsumerAPI.Services
{
    public interface IJokePersistenceService
    {
        Task<bool> TrySaveJokeFromJsonAsync(string message, CancellationToken cancellationToken = default);
    }
}
