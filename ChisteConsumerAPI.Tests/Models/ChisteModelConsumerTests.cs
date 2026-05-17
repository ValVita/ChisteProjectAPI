using ChisteConsumerAPI.Models;

namespace ChisteConsumerAPI.Tests.Models;

public class ChisteModelConsumerTests
{
    [Fact]
    public void ChisteModelConsumer_StoresPropertyValues()
    {
        var createdAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var model = new ChisteModelConsumer
        {
            Id = 10,
            ExternalId = "ext",
            Content = "contenido",
            CreatedAt = createdAt
        };

        Assert.Equal(10, model.Id);
        Assert.Equal("ext", model.ExternalId);
        Assert.Equal("contenido", model.Content);
        Assert.Equal(createdAt, model.CreatedAt);
    }

    [Fact]
    public void ChisteModelConsumer_DefaultCreatedAt_IsUtcNow()
    {
        var before = DateTime.UtcNow.AddSeconds(-1);
        var model = new ChisteModelConsumer();
        var after = DateTime.UtcNow.AddSeconds(1);

        Assert.InRange(model.CreatedAt, before, after);
    }
}
