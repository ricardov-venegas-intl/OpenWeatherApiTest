# OpenWeatherApiTest

This is a simple test of the https://openweathermap.org/ API.


## Requirements:
Weather Service Assignment / Project
Write an http server that uses the Open Weather API that exposes an endpoint that takes in lat/long coordinates. This endpoint should return: what the weather condition is outside in that area (snow, rain, etc),  whether it’s hot, cold, or moderate outside (use your own discretion on what temperature equates to each type and whether there are any weather alerts going on in that area.

## Implementation Notes

EndPoint: https://localhost:5001/WeatherForecast
Parameters: latitude= & longitud

Wep APi Response (WeatherForecast):
* TemperatureC: Temperature in Celsius.
* TemperatureF: Temperature in Farenheight.
* WeatherCondition: What the weather condition is outside in that area (snow, rain, etc) () Based on https://openweathermap.org/weather-conditions#How-to-get-icon-URL.
* Summary: Whether it’s hot, cold, or moderate outside: If the temp is between 0-50 its cold, 50-75 moderate, 75+ Hot) 
* Alerts: Text field that indicates Whether there are any weather alerts going on in that area, and list them.

## Notes

* The project/repository runs build and tests on Pull Requests and Merges.
* Manual tests can be done using swagger when running: https://localhost:5001/swagger/index.html

## Main Components

* WeatherForecastController: Api controller (endpoints)
* OpenWeatherMapClient:  Client for https://openweathermap.org/ . No automated tests designed because no sandbox is provided for openweathermap.org. 
* OneCallResponseToWeatherForecastMapper: Mas the response from openweathermap.org in the reponse for this web api 

## Configuration
Set api key using the enviroment
E.g.
$env:OpenWeatherMapClientConfiguration_ApiKey="key Value"

You get the key value when registed with https://openweathermap.org/ 

