using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenWeatherApiTest.Controllers;
using OpenWeatherApiTest.OpenWeatherMapApi;
using OpenWeatherApiTest.OpenWeatherMapApi.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWeatherApiTest.Tests
{
    /// <summary>
    /// Weather Forecast Controller Tests
    /// </summary>
    [TestClass]
    public class WeatherForecastControllerTests
    {
        /// <summary>
        /// Temperatures And Summary Tests
        /// </summary>
        /// <param name="celsius">Temperature.</param>
        /// <param name="summary">Expected Summary.</param>
        /// <returns>Task</returns>
        [TestMethod]
        [DataRow(-33, "Freezing")]
        [DataRow(-21, "Freezing")]
        [DataRow(-20, "Bracing")]
        [DataRow(-15, "Bracing")]
        [DataRow(-11, "Bracing")]
        [DataRow(-10, "Chilly")]
        [DataRow(0, "Chilly")]
        [DataRow(4, "Chilly")]
        [DataRow(5, "Cool")]
        [DataRow(7, "Cool")]
        [DataRow(9, "Cool")]
        [DataRow(10, "Mild")]
        [DataRow(13, "Mild")]
        [DataRow(14, "Mild")]
        [DataRow(15, "Warm")]
        [DataRow(17, "Warm")]
        [DataRow(24, "Warm")]
        [DataRow(25, "Balmy")]
        [DataRow(27, "Balmy")]
        [DataRow(29, "Balmy")]
        [DataRow(30, "Hot")]
        [DataRow(33, "Hot")]
        [DataRow(34, "Hot")]
        [DataRow(35, "Sweltering")]
        [DataRow(37, "Sweltering")]
        [DataRow(39, "Sweltering")]
        [DataRow(40, "Scorching")]
        [DataRow(41, "Scorching")]
        [DataRow(55, "Scorching")]
        public async Task TestTemperaturesAndSummary(int celsius, string summary)
        {
            // Arrange
            var openWeatherMapClient = new Mock<IOpenWeatherMapClient>();
            openWeatherMapClient.Setup(o => o.OneCall(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    new OneCallResponse
                    {
                        CurrentWeather = new CurrentWeather
                        {
                            Temperature = celsius
                        },
                        Alerts = null
                    });

            var logger = new Mock<ILogger<WeatherForecastController>>();

            // Act
            WeatherForecastController controller = new WeatherForecastController(
                openWeatherMapClient.Object,
                logger.Object);

            var result = await controller.Get(1.0m, -1.0m);

            // Assert
            Assert.AreEqual(celsius, result.TemperatureC);
            Assert.AreEqual(32 + (int)(celsius * 1.8), result.TemperatureF);
            Assert.AreEqual(summary, result.Summary);
        }


        /// <summary>
        /// Test result with Alerts
        /// </summary>
        /// <returns>Task</returns>
        [TestMethod]
        public async Task BasicTestWithAlerts()
        {
            // Arrange
            var openWeatherMapClient = new Mock<IOpenWeatherMapClient>();
            openWeatherMapClient.Setup(o =>o.OneCall(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    new OneCallResponse
                    {
                        CurrentWeather = new CurrentWeather
                        {
                            Temperature = 0
                        },
                        Alerts = new List<Alert>()
                        {
                            new Alert
                            { 
                                Event = "Blah", 
                                Sender = "QQQQ", 
                                Description= "Lorem ipsum dolor sit amet"
                            },
                            new Alert
                            {
                                Event = "ZZZZ",
                                Sender = "RRRR",
                                Description= "In iaculis nunc sed augue lacus viverra vitae."
                            }
                        }
                    });

            var logger = new Mock<ILogger<WeatherForecastController>>();

            // Act
            WeatherForecastController controller = new WeatherForecastController(
                openWeatherMapClient.Object, 
                logger.Object);

            var result = await controller.Get(1.0m, -1.0m);

            // Assert
            Assert.AreEqual(0, result.TemperatureC);
            Assert.AreEqual(32, result.TemperatureF);
            Assert.AreEqual("Chilly", result.Summary);
            Assert.AreEqual("Blah\r\nLorem ipsum dolor sit amet\r\n\r\nZZZZ\r\nIn iaculis nunc sed augue lacus viverra vitae.\r\n", result.Alerts);
        }
    }
}
