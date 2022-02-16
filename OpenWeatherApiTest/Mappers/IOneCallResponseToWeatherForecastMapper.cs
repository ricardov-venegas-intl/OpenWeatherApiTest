using OpenWeatherApiTest.Models;
using OpenWeatherApiTest.OpenWeatherMapApi.Models;

namespace OpenWeatherApiTest.Mappers
{
    public interface IOneCallResponseToWeatherForecastMapper
    {
        WeatherForecast Map(OneCallResponse source);
    }
}