using OpenWeatherApiTest.Models;
using OpenWeatherApiTest.OpenWeatherMapApi.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherApiTest.Mappers
{
    /// <summary>
    /// OneCallResponse To WeatherForecast Mapper
    /// </summary>
    public class OneCallResponseToWeatherForecastMapper : IOneCallResponseToWeatherForecastMapper
    {
        /// <summary>
        /// Maps a OneCallResponse instance into a WeatherForecast instance.
        /// </summary>
        /// <param name="source">OneCallResponse instance from Openweathermap OneCall</param>
        /// <returns></returns>
        public WeatherForecast Map(OneCallResponse source)
        {
            var result = new WeatherForecast();
            result.WeatherCondition = (source.CurrentWeather.Weather?[0].Main ?? "None");
            result.TemperatureC = Convert.ToInt32(source.CurrentWeather.Temperature);
            result.Summary = MapTemperatureToSummary(source);
            result.Alerts = MergeWeatherAlerts(source.Alerts);
            return result;
        }

        /// <summary>
        /// Maps a temperature to Summery
        /// </summary>
        /// <param name="source">Response from IOpenWeatherMapClient.OneCall</param>
        /// <returns>String with the appropiate summery based on temperature</returns>
        private string MapTemperatureToSummary(OpenWeatherMapApi.Models.OneCallResponse source)
        {
            // Notes we expect source in celcius
            // 50F = 10C
            // 75F = 24C
            return source.CurrentWeather.Temperature <= 10 ? "Cold" :
                            source.CurrentWeather.Temperature <= 24 ? "Moderate" :
                            "Hot";
        }

        /// <summary>
        /// Merge all alarts iunto an text
        /// </summary>
        /// <param name="alerts">Alerts to merge</param>
        /// <returns>String with the alert description</returns>
        private string MergeWeatherAlerts(IEnumerable<Alert> alerts)
        {
            if (alerts == null)
            {
                return "No Alerts";
            }

            var sb = new StringBuilder();
            var addSeparationLine = false;
            foreach (Alert alert in alerts)
            {
                if (addSeparationLine == true)
                {
                    sb.AppendLine();
                }
                sb.AppendLine($"Source: {alert.Sender}");
                sb.AppendLine($"Event: {alert.Event}");
                sb.AppendLine($"Description: {alert.Description}");
                addSeparationLine = true;
            }

            return sb.ToString();
        }
    }
}
