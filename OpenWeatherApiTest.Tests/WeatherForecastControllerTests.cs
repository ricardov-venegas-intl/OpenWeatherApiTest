using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using OpenWeatherApiTest.Controllers;
using OpenWeatherApiTest.Mappers;
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
        /// Test result with Alerts
        /// </summary>
        /// <returns>Task</returns>
        [TestMethod]
        public async Task BasicTestWithNoAlerts()
        {
            // Arrange
            var mapper = new OneCallResponseToWeatherForecastMapper();
            var openWeatherMapClient = new Mock<IOpenWeatherMapClient>();
            openWeatherMapClient.Setup(o => o.OneCall(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    new OneCallResponse
                    {
                        CurrentWeather = new CurrentWeather
                        {
                            Temperature = 0,
                            Weather = new List<Weather> {
                                new Weather
                                {
                                    Id = 232,
                                    Main = "Thunderstorm",
                                    Description = "thunderstorm with heavy drizzle"
                                }
                            }
                        },
                        Alerts = null,
                    }); 

            var logger = new Mock<ILogger<WeatherForecastController>>();

            // Act
            WeatherForecastController controller = new WeatherForecastController(
                openWeatherMapClient.Object,
                mapper,
                logger.Object);

            var result = await controller.Get(1.0m, -1.0m);

            // Assert
            Assert.AreEqual(0, result.TemperatureC);
            Assert.AreEqual(32, result.TemperatureF);
            Assert.AreEqual("Cold", result.Summary);
            Assert.AreEqual("No Alerts", result.Alerts);
            Assert.AreEqual("Thunderstorm", result.WeatherCondition);
        }

        /// <summary>
        /// Test result with Alerts
        /// </summary>
        /// <returns>Task</returns>
        [TestMethod]
        public async Task BasicTestWithAlerts()
        {
            // Arrange
            var mapper = new OneCallResponseToWeatherForecastMapper();
            var openWeatherMapClient = new Mock<IOpenWeatherMapClient>();
            openWeatherMapClient.Setup(o =>o.OneCall(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    new OneCallResponse
                    {
                        CurrentWeather = new CurrentWeather
                        {
                            Temperature = 0,
                            Weather = new List<Weather> {
                                new Weather
                                {
                                    Id = 232,
                                    Main = "Thunderstorm",
                                    Description = "thunderstorm with heavy drizzle"
                                }
                            }
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
                mapper,
                logger.Object);

            var result = await controller.Get(1.0m, -1.0m);

            // Assert
            Assert.AreEqual(0, result.TemperatureC);
            Assert.AreEqual(32, result.TemperatureF);
            Assert.AreEqual("Cold", result.Summary);
            Assert.AreEqual("Source: QQQQ\nEvent: Blah\nDescription: Lorem ipsum dolor sit amet\n\nSource: RRRR\nEvent: ZZZZ\nDescription: In iaculis nunc sed augue lacus viverra vitae.\n", result.Alerts.Replace("\r\n", "\n"));
            Assert.AreEqual("Thunderstorm", result.WeatherCondition);
        }

        /// <summary>
        /// Test result with Alerts
        /// </summary>
        /// <returns>Task</returns>
        [TestMethod]
        public async Task BasicTestWithMultipleWeatherCondition()
        {
            // Arrange
            var mapper = new OneCallResponseToWeatherForecastMapper();
            var openWeatherMapClient = new Mock<IOpenWeatherMapClient>();
            openWeatherMapClient.Setup(o => o.OneCall(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    new OneCallResponse
                    {
                        CurrentWeather = new CurrentWeather
                        {
                            Temperature = 0,
                            Weather = new List<Weather> {
                                new Weather
                                {
                                    Id = 232,
                                    Main = "Thunderstorm",
                                    Description = "thunderstorm with heavy drizzle"
                                },
                                                                new Weather
                                {
                                    Id = 611,
                                    Main = "Snow",
                                    Description = "Sleet"
                                }
                            }
                        },
                        Alerts = new List<Alert>()
                        {
                            new Alert
                            {
                                Event = "Blah",
                                Sender = "QQQQ",
                                Description= "Lorem ipsum dolor sit amet"
                            }
                        }
                    });

            var logger = new Mock<ILogger<WeatherForecastController>>();

            // Act
            WeatherForecastController controller = new WeatherForecastController(
                openWeatherMapClient.Object,
                mapper,
                logger.Object);

            var result = await controller.Get(1.0m, -1.0m);

            // Assert
            Assert.AreEqual(0, result.TemperatureC);
            Assert.AreEqual(32, result.TemperatureF);
            Assert.AreEqual("Cold", result.Summary);
            Assert.AreEqual("Source: QQQQ\nEvent: Blah\nDescription: Lorem ipsum dolor sit amet\n", result.Alerts.Replace("\r\n", "\n"));
            Assert.AreEqual("Thunderstorm", result.WeatherCondition);
        }

        [TestMethod]
        public void WeatherForecastControllerConstructorArgumentVerification()
        {
            // Arrange
            var mapper = new OneCallResponseToWeatherForecastMapper();
            var openWeatherMapClient = new Mock<IOpenWeatherMapClient>();
            var logger = new Mock<ILogger<WeatherForecastController>>();

            // Act/Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new WeatherForecastController(
                    null,
                    mapper,
                    logger.Object));

            Assert.ThrowsException<ArgumentNullException>(
                () => new WeatherForecastController(
                    openWeatherMapClient.Object,
                    null,
                    logger.Object));

            Assert.ThrowsException<ArgumentNullException>(
                () => new WeatherForecastController(
                    openWeatherMapClient.Object,
                    mapper,
                    null));
        }
    }
}
