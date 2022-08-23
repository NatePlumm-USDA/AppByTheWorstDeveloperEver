using System;
using AppByTheWorstDeveloperEver;
using Xunit;

namespace MyUnitTestProject
{
    public class WeatherForecastTest
    {
        [Fact]
        public void TestConversionToF()
        {
            WeatherForecast forecast = new WeatherForecast {TemperatureC = 30};

            Assert.Equal(85, forecast.TemperatureF);
        }
    }
}
