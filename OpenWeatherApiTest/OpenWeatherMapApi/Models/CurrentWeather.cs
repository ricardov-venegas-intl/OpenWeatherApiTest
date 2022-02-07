using Newtonsoft.Json;

namespace OpenWeatherApiTest.OpenWeatherMapApi.Models
{

    /// <summary>
    /// Current Weather 
    /// </summary>
    public class CurrentWeather
    {
        /// <summary>
        /// Temperature.
        /// </summary>
        [JsonProperty("temp")]
        public decimal Temperature { get; set; }
    }
}
