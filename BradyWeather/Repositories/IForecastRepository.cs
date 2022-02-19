using BradyWeather.OpenWeatherMapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradyWeather.Repositories
{
    public interface IForecastRepository
    {
        List<WeatherResponse> GetForecast(List<GeoCodingResponse> coordinates);

        List<GeoCodingResponse> GetCoordinates(string city);

    }
}
