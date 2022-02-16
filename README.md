# OpenWeatherApiTest

This is a simple test of the https://openweathermap.org/ API.

Set api key using the enviroment

E.g.
$env:OpenWeatherMapClientConfiguration_ApiKey="fff0000aaaa11111234cccc1234f"

## Requirements:
Weather Service Assignment / Project
Write an http server that uses the Open Weather API that exposes an endpoint that takes in lat/long coordinates. (Does Server take in Coordinates?)
This endpoint should return:
* What the weather condition is outside in that area (snow, rain, etc),  (Found this webpage – it may help https://openweathermap.org/weather-conditions#How-to-get-icon-URL) 
* whether it’s hot, cold, or moderate outside (use your own discretion on what temperature equates to each type), (So for this you’d basically need to set it up so that if the temp is between 0-50 its cold, 50-75 moderate, 75+ Hot) 
* Whether there are any weather alerts going on in that area,
o	with what is going on if there is currently an active alert. (https://openweathermap.org/api/one-call-api#listsource)

The API can be found here: https://openweathermap.org/api. 
The one-call api returns all of the data while the other apis are piece-mealed sections. 
You may also find the https://openweathermap.org/faq useful. 
