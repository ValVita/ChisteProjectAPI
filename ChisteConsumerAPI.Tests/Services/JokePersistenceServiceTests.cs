using ChisteConsumerAPI.Data;
using ChisteConsumerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChisteConsumerAPI.Tests.Services;

public class JokePersistenceServiceTests
{
    private static (IServiceProvider Provider, string DbName) CreateServiceProvider()
    {
        var dbName = Guid.NewGuid().ToString();
        var services = new ServiceCollection();
        services.AddDbContext<ChisteDbContext>(options =>
            options.UseInMemoryDatabase(dbName));
        return (services.BuildServiceProvider(), dbName);
    }

    [Fact]
    public async Task TrySaveJokeFromJsonAsync_ReturnsTrue_AndPersistsJoke()
    {
        var (provider, _) = CreateServiceProvider();
        var service = new JokePersistenceService(provider);
        var json = """{"Id":"joke-1","Value":"Un chiste","Url":"http://test"}""";

        var saved = await service.TrySaveJokeFromJsonAsync(json);

        Assert.True(saved);
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ChisteDbContext>();
        var jokes = await context.CHISTEDB.ToListAsync();
        Assert.Single(jokes);
        Assert.Equal("joke-1", jokes[0].ExternalId);
        Assert.Equal("Un chiste", jokes[0].Content);
    }

    [Fact]
    public async Task TrySaveJokeFromJsonAsync_ReturnsFalse_WhenJsonIsInvalid()
    {
        var (provider, _) = CreateServiceProvider();
        var service = new JokePersistenceService(provider);

        var saved = await service.TrySaveJokeFromJsonAsync("not-json");

        Assert.False(saved);
    }

    [Fact]
    public async Task TrySaveJokeFromJsonAsync_ReturnsFalse_WhenJsonIsNullLiteral()
    {
        var (provider, _) = CreateServiceProvider();
        var service = new JokePersistenceService(provider);

        var saved = await service.TrySaveJokeFromJsonAsync("null");

        Assert.False(saved);
    }
}
