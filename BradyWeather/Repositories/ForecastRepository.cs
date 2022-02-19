using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BradyWeather.OpenWeatherMapModels;
using Microsoft.VisualBasic;
using RestSharp;
using BradyWeather.Config;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BradyWeather.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        List<WeatherResponse> IForecastRepository.GetForecast(List<GeoCodingResponse> coord)
        {
            string IDOWeather = Config.Constants.OPEN_WEATHER_APPID;

            List<WeatherResponse> answer = new List<WeatherResponse>();

            for (int i = 0; i < coord.Count; i++)
            {
                var client = new RestClient($"http://api.openweathermap.org/data/2.5/weather?lat={coord[i].Lat}&lon={coord[i].Lon}&appid={IDOWeather}&units=metric");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    // Deserialize the string content into JToken object
                    var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                    // Deserialize the JToken object into our WeatherResponse Class
                    answer.Add(content.ToObject<WeatherResponse>());
                }
            }
            if (answer != null)
            {
                return answer;
            }
            // Connection String
            return null;
        }

        List<GeoCodingResponse> IForecastRepository.GetCoordinates(string city)
        {
            string IDOWeather = Config.Constants.OPEN_WEATHER_APPID;
            // Connection String
            var client = new RestClient($"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid={IDOWeather}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Deserialize the string content into JToken object
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                // Deserialize the JToken object into our coordinate Class
                return content.ToObject<List<GeoCodingResponse>>();
            }

            return null;
        }
    }
}
