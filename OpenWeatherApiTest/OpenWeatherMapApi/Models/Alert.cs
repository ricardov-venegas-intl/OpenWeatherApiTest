using Newtonsoft.Json;

namespace OpenWeatherApiTest.OpenWeatherMapApi.Models
{
    /// <summary>
    /// Alert.
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Sender.
        /// </summary>
        [JsonProperty("sender_name")]
        public string Sender { get; set; }

        /// <summary>
        /// Event.
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
