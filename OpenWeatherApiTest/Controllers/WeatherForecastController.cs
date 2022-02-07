using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using OpenWeatherApiTest.OpenWeatherMapApi;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherApiTest.Controllers
{
    /// <summary>
    /// Weather Forecast Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOpenWeatherMapClient _openWeatherMapClient;
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initiales an instance of WeatherForecastController
        /// </summary>
        /// <param name="openWeatherMapClient">IOpenWeatherMapClient instance</param>
        /// <param name="logger">ILogger</param>
        public WeatherForecastController(
            IOpenWeatherMapClient openWeatherMapClient,
            ILogger<WeatherForecastController> logger)
        {
            _openWeatherMapClient = openWeatherMapClient;
            _logger = logger;
        }

        /// <summary>
        /// Get the Weather for a place in earth.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitud">Longitud.</param>
        /// <returns>A WeatherForecast instance.</returns>
        [HttpGet]
        public async Task<WeatherForecast> Get(decimal latitude, decimal longitud)
        {
            try
            {
                _logger.LogInformation("Getting WeatherForecast");

                var response = await _openWeatherMapClient.OneCall(latitude, longitud);
                var result = new WeatherForecast();
                result.Date = DateTime.Now;
                result.TemperatureC = Convert.ToInt32(response.CurrentWeather.Temperature);
                result.Summary = MapTemperatureToSummary(response);
                if (response.Alerts != null)
                {
                    var alertsTexts = response.Alerts.Select(a => a.Event + "\r\n" + a.Description + "\r\n");
                    result.Alerts = String.Join("\r\n", alertsTexts);
                }
                else
                {
                    result.Alerts = string.Empty;
                }

                result.WeatherCondition = (response.CurrentWeather.Weather?[0].Main ?? "None")
                    + " - "
                    + (response.CurrentWeather.Weather?[0].Description ?? string.Empty);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting WeatherForecast");
                throw;
            }
        }

        /// <summary>
        /// Maps a temperature to Summery
        /// </summary>
        /// <param name="response">Response form IOpenWeatherMapClient</param>
        /// <returns>String with the appropiate summery based on temperature</returns>
        private static string MapTemperatureToSummary(OpenWeatherMapApi.Models.OneCallResponse response)
        {
            return response.CurrentWeather.Temperature < -20 ? "Freezing" :
                            response.CurrentWeather.Temperature < -10 ? "Bracing" :
                            response.CurrentWeather.Temperature < 5 ? "Chilly" :
                            response.CurrentWeather.Temperature < 10 ? "Cool" :
                            response.CurrentWeather.Temperature < 15 ? "Mild" :
                            response.CurrentWeather.Temperature < 25 ? "Warm" :
                            response.CurrentWeather.Temperature < 30 ? "Balmy" :
                            response.CurrentWeather.Temperature < 35 ? "Hot" :
                            response.CurrentWeather.Temperature < 40 ? "Sweltering" :
                            "Scorching";
        }
    }
}
