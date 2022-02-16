using Newtonsoft.Json;

using OpenWeatherApiTest.OpenWeatherMapApi.Models;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherApiTest.OpenWeatherMapApi.Implementation
{
    /// <summary>
    /// OpenWeatherMapClient for https://openweathermap.org/ API.
    /// </summary>
    public class OpenWeatherMapClient : IOpenWeatherMapClient
    {
        OpenWeatherMapClientConfiguration clientConfiguration;

        /// <summary>
        /// Initializes an instance of OpenWeatherMapClient
        /// </summary>
        /// <param name="clientConfiguration"></param>
        public OpenWeatherMapClient(OpenWeatherMapClientConfiguration clientConfiguration)
        {
            _= clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));

            this.clientConfiguration = clientConfiguration;
        }

        /// <summary>
        /// Call One Call API
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitud">Longitud.</param>
        /// <returns>OneCallResponse</returns>
        public async Task<OneCallResponse> OneCall(decimal latitude, decimal longitud)
        {
            using (var httpClient = new HttpClient())
            {
                Uri url = new Uri($"{clientConfiguration.Urlbase}?lat={latitude}&lon={longitud}&appid={clientConfiguration.ApiKey}&units=metric");
                var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var oneCallResponse = JsonConvert.DeserializeObject<OneCallResponse>(content);
                return oneCallResponse;
            }                
        }
    }
}
