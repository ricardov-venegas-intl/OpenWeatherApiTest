using System;

namespace OpenWeatherApiTest.Models
{
    /// <summary>
    /// WeatherForecast.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Temperature in Celsious.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperature in Farenheight.
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC * 1.8);

        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Weather Condition.
        /// </summary>
        public string WeatherCondition { get; set; }

        /// <summary>
        /// Weather Alerts for the zone
        /// </summary>
        public string Alerts { get; set; }
    }
}
