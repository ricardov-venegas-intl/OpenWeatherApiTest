using Newtonsoft.Json;

using System.Collections.Generic;

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

        /// <summary>
        /// Weather.
        /// </summary>
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
    }
}
