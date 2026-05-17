using ChisteFetcherAPI.Controllers;
using ChisteFetcherAPI.Models;
using ChisteFetcherAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChisteFetcherAPI.Tests.Controllers;

public class JokesControllerTests
{
    private readonly Mock<IChisteService> _chisteServiceMock = new();
    private readonly Mock<IRabbitMqPublisher> _publisherMock = new();

    private JokesController CreateController() =>
        new(_chisteServiceMock.Object, _publisherMock.Object);

    [Fact]
    public async Task GetAndSendJoke_ReturnsOk_WhenJokeIsRetrieved()
    {
        var joke = new ChisteModel { Id = "1", Value = "Test joke", Url = "http://test" };
        _chisteServiceMock.Setup(s => s.GetRandomJokeAsync()).ReturnsAsync(joke);
        _publisherMock.Setup(p => p.PublishJokeAsync(joke)).Returns(Task.CompletedTask);

        var result = await CreateController().GetAndSendJoke();

        var okResult = Assert.IsType<OkObjectResult>(result);
        _publisherMock.Verify(p => p.PublishJokeAsync(joke), Times.Once);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task GetAndSendJoke_Returns500_WhenJokeIsNull()
    {
        _chisteServiceMock.Setup(s => s.GetRandomJokeAsync()).ReturnsAsync((ChisteModel?)null);

        var result = await CreateController().GetAndSendJoke();

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
        _publisherMock.Verify(p => p.PublishJokeAsync(It.IsAny<ChisteModel>()), Times.Never);
    }
}
