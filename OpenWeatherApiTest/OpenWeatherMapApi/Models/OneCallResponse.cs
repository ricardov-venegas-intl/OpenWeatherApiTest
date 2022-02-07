
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenWeatherApiTest.OpenWeatherMapApi.Models
{
    public class OneCallResponse
    {
        [JsonProperty("current")]
        public CurrentWeather CurrentWeather { get; set; }

        [JsonProperty("alerts")]
        public List<Alert> Alerts { get; set; }
    }
}
