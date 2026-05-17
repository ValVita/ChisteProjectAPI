using ChisteConsumerAPI.Controllers;
using ChisteConsumerAPI.Data;
using ChisteConsumerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChisteConsumerAPI.Tests.Controllers;

public class JokesControllerTests
{
    private static ChisteDbContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ChisteDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new ChisteDbContext(options);
    }

    [Fact]
    public async Task GetAllJokes_ReturnsOkWithJokes()
    {
        await using var context = CreateContext(nameof(GetAllJokes_ReturnsOkWithJokes));
        context.CHISTEDB.Add(new ChisteModelConsumer
        {
            ExternalId = "ext-1",
            Content = "Chiste de prueba",
            CreatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var controller = new JokesController(context);

        var result = await controller.GetAllJokes();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var jokes = Assert.IsAssignableFrom<List<ChisteModelConsumer>>(okResult.Value);
        Assert.Single(jokes);
        Assert.Equal("ext-1", jokes[0].ExternalId);
        Assert.Equal("Chiste de prueba", jokes[0].Content);
    }

    [Fact]
    public async Task GetAllJokes_ReturnsEmptyList_WhenNoJokesExist()
    {
        await using var context = CreateContext(nameof(GetAllJokes_ReturnsEmptyList_WhenNoJokesExist));
        var controller = new JokesController(context);

        var result = await controller.GetAllJokes();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var jokes = Assert.IsAssignableFrom<List<ChisteModelConsumer>>(okResult.Value);
        Assert.Empty(jokes);
    }
}
