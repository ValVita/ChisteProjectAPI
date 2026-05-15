using ChisteFetcherAPI.Models;

namespace ChisteFetcherAPI.Services
{
    public class ChisteService : IChisteService
    {
        private readonly HttpClient _httpClient;
        public ChisteService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<ChisteModel?> GetRandomJokeAsync()
        {
            var response = await _httpClient.GetAsync("https://api.chucknorris.io/jokes/random");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ChisteModel>();
        }
    }
}
