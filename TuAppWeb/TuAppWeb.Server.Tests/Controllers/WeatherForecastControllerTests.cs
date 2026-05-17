using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TuAppWeb.Server;
using TuAppWeb.Server.Controllers;

namespace TuAppWeb.Server.Tests.Controllers;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsFiveForecasts()
    {
        var logger = new Mock<ILogger<WeatherForecastController>>();
        var controller = new WeatherForecastController(logger.Object);

        var forecasts = controller.Get().ToList();

        Assert.Equal(5, forecasts.Count);
    }

    [Fact]
    public void Get_ReturnsForecastsWithValidSummaries()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        var logger = new Mock<ILogger<WeatherForecastController>>();
        var controller = new WeatherForecastController(logger.Object);

        var forecasts = controller.Get().ToList();

        Assert.All(forecasts, f =>
        {
            Assert.Contains(f.Summary, summaries);
            Assert.True(f.TemperatureC >= -20 && f.TemperatureC < 55);
            Assert.True(f.Date > DateOnly.FromDateTime(DateTime.Now));
        });
    }
}
