using ChisteConsumerAPI.Data;
using ChisteConsumerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChisteConsumerAPI.Tests.Data;

public class ChisteDbContextTests
{
    [Fact]
    public async Task ChisteDbContext_CanAddAndQueryJokes()
    {
        var options = new DbContextOptionsBuilder<ChisteDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new ChisteDbContext(options);
        context.CHISTEDB.Add(new ChisteModelConsumer { ExternalId = "1", Content = "A" });
        await context.SaveChangesAsync();

        var count = await context.CHISTEDB.CountAsync();
        Assert.Equal(1, count);
    }
}
