using ChisteFetcherAPI.Models;

namespace ChisteFetcherAPI.Services
{
    public interface IChisteService
    {
        Task<ChisteModel?> GetRandomJokeAsync();
    }
}
