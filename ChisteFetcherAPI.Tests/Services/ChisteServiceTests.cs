using System.Net;
using System.Text.Json;
using ChisteFetcherAPI.Models;
using ChisteFetcherAPI.Services;

namespace ChisteFetcherAPI.Tests.Services;

public class ChisteServiceTests
{
    [Fact]
    public async Task GetRandomJokeAsync_ReturnsJoke_WhenApiRespondsSuccessfully()
    {
        var expected = new ChisteModel
        {
            Id = "abc",
            Value = "Chuck Norris can divide by zero.",
            Url = "https://api.chucknorris.io/jokes/abc"
        };
        var json = JsonSerializer.Serialize(expected);

        var handler = new MockHttpMessageHandler(_ =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            });

        var service = new ChisteService(new HttpClient(handler));

        var result = await service.GetRandomJokeAsync();

        Assert.NotNull(result);
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Value, result.Value);
        Assert.Equal(expected.Url, result.Url);
        Assert.Equal("https://api.chucknorris.io/jokes/random", handler.LastRequestUri?.ToString());
    }

    [Fact]
    public async Task GetRandomJokeAsync_Throws_WhenApiReturnsError()
    {
        var handler = new MockHttpMessageHandler(_ =>
            new HttpResponseMessage(HttpStatusCode.InternalServerError));

        var service = new ChisteService(new HttpClient(handler));

        await Assert.ThrowsAsync<HttpRequestException>(() => service.GetRandomJokeAsync());
    }

    private sealed class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _handler;

        public Uri? LastRequestUri { get; private set; }

        public MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            _handler = handler;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            LastRequestUri = request.RequestUri;
            return Task.FromResult(_handler(request));
        }
    }
}
