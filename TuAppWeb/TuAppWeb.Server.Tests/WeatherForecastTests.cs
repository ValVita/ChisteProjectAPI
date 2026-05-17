using TuAppWeb.Server;

namespace TuAppWeb.Server.Tests;

public class WeatherForecastTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(25)]
    public void TemperatureF_ConvertsFromCelsius(int celsius)
    {
        var forecast = new WeatherForecast { TemperatureC = celsius };
        var expected = 32 + (int)(celsius / 0.5556);

        Assert.Equal(expected, forecast.TemperatureF);
    }

    [Fact]
    public void WeatherForecast_StoresProperties()
    {
        var date = DateOnly.FromDateTime(DateTime.Today);
        var forecast = new WeatherForecast
        {
            Date = date,
            TemperatureC = 25,
            Summary = "Warm"
        };

        Assert.Equal(date, forecast.Date);
        Assert.Equal(25, forecast.TemperatureC);
        Assert.Equal("Warm", forecast.Summary);
    }
}
