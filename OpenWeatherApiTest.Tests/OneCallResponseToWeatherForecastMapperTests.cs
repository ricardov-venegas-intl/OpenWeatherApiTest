using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenWeatherApiTest.Mappers;
using OpenWeatherApiTest.OpenWeatherMapApi.Models;

using System.Collections.Generic;

namespace OpenWeatherApiTest.Tests
{
    /// <summary>
    /// OneCallResponseToWeatherForecastMapper Tests
    /// </summary>
    [TestClass]
    public class OneCallResponseToWeatherForecastMapperTests
    {
        /// <summary>
        /// Temperatures And Summary Tests
        /// </summary>
        /// <param name="celsius">Temperature.</param>
        /// <param name="summary">Expected Summary.</param>
        /// <returns>Task</returns>
        [TestMethod]
        [DataRow(-33, "Cold")]
        [DataRow(10, "Cold")] // Limit Condition for Cold
        [DataRow(11, "Moderate")] // Limit Condition for Moderate
        [DataRow(13, "Moderate")]
        [DataRow(24, "Moderate")] // Limit Condition for Moderate
        [DataRow(25, "Hot")] // Limit Condition for Moderate
        [DataRow(55, "Hot")]
        public void TestTemperaturesAndSummary(int celsius, string summary)
        {
            // Arrange
            var mapper = new OneCallResponseToWeatherForecastMapper();
            var responseFromApi = new OneCallResponse()
            {
                CurrentWeather = new CurrentWeather()
                {
                    Temperature = celsius,
                    Weather = new List<Weather>
                    {
                        new Weather
                        {
                            Id = 123,
                            Main = "Rain",
                            Description = "light rain"
                        }
                    },
                },
                Alerts = null
            };

            // Act
            var result = mapper.Map(responseFromApi);

            // Assert
            Assert.AreEqual(celsius, result.TemperatureC);
            Assert.AreEqual(32 + (int)(celsius * 1.8), result.TemperatureF);
            Assert.AreEqual(summary, result.Summary);
            Assert.AreEqual("Rain", result.WeatherCondition);
        }
    }
}
