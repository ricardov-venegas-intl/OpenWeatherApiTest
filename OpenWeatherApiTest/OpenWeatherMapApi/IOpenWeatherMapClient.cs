using OpenWeatherApiTest.OpenWeatherMapApi.Models;

using System.Threading.Tasks;

namespace OpenWeatherApiTest.OpenWeatherMapApi
{
    /// <summary>
    /// Interface for https://openweathermap.org/ API.
    /// </summary>
    public interface IOpenWeatherMapClient
    {
        /// <summary>
        /// Call One Call API
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitud">Longitud.</param>
        /// <returns>OneCallResponse</returns>
        Task<OneCallResponse> OneCall(decimal latitude, decimal longitud);
    }
}
