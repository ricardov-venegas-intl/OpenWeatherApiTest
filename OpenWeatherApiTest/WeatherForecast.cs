using System;

namespace OpenWeatherApiTest
{
    /// <summary>
    /// WeatherForecast.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Date of the forecast.
        /// </summary>
        public DateTime Date { get; set; }

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
        /// Weather Alerts for the zone
        /// </summary>
        public string Alerts { get; set; }
    }
}
