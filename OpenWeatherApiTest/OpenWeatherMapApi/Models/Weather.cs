using Newtonsoft.Json;

namespace OpenWeatherApiTest.OpenWeatherMapApi.Models
{
    /// <summary>
    /// Weather
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// Id.
        /// See https://openweathermap.org/weather-conditions#Weather-Condition-Codes-2
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Main description.
        /// </summary>
        [JsonProperty("main")]
        public string Main { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
