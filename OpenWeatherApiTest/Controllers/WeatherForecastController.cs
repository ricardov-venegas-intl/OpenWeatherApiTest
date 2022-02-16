using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using OpenWeatherApiTest.Mappers;
using OpenWeatherApiTest.Models;
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
        private readonly IOneCallResponseToWeatherForecastMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initiales an instance of WeatherForecastController
        /// </summary>
        /// <param name="openWeatherMapClient">IOpenWeatherMapClient instance</param>
        /// <param name="logger">ILogger</param>
        public WeatherForecastController(
            IOpenWeatherMapClient openWeatherMapClient,
            IOneCallResponseToWeatherForecastMapper mapper,
            ILogger<WeatherForecastController> logger)
        {
            _ = openWeatherMapClient ?? throw new ArgumentNullException(nameof(openWeatherMapClient));
            _ = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ = logger ?? throw new ArgumentNullException(nameof(logger));

            _openWeatherMapClient = openWeatherMapClient;
            _mapper = mapper;
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
                return _mapper.Map(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting WeatherForecast");
                throw;
            }
        }        
    }
}
