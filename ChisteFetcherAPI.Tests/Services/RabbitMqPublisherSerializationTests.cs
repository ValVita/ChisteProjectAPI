using System.Text;
using System.Text.Json;
using ChisteFetcherAPI.Models;

namespace ChisteFetcherAPI.Tests.Services;

public class RabbitMqPublisherSerializationTests
{
    [Fact]
    public void JokeSerialization_ProducesExpectedJson()
    {
        var joke = new ChisteModel { Id = "1", Value = "Test", Url = "http://test" };

        var message = JsonSerializer.Serialize(joke);
        var body = Encoding.UTF8.GetBytes(message);
        var deserialized = JsonSerializer.Deserialize<ChisteModel>(Encoding.UTF8.GetString(body));

        Assert.Equal(joke.Id, deserialized!.Id);
        Assert.Equal(joke.Value, deserialized.Value);
        Assert.Equal(joke.Url, deserialized.Url);
    }
}
