using BradyWeather.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BradyWeather.OpenWeatherMapModels;
using BradyWeather.Repositories;

namespace BradyWeather.Controllers
{
        //RK this will require every method inside home controller can be accessed only if you authorized except Index() see bellow, which will redirect to home page
    //[Authorize] todo temp commented out
    public class WeatherController : Controller
    {
        private readonly IForecastRepository _forecastRepository;

        // Dependency Injection
        public WeatherController(IForecastRepository forecastAppRepo)
        {
            _forecastRepository = forecastAppRepo;
        }

        // GET: BradyWeather/SearchCity
        public IActionResult SearchCity()
        {
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        // POST: BradyWeather/SearchCity
        [HttpPost]
        public IActionResult SearchCity(SearchCity model)
        {
            // If the model is valid, consume the Weather API to bring the data of the city
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "Weather", new { city = model.CityName });
            }
            return View(model);
        }

        // GET: BradyWeather/City
        public IActionResult City(string city)
        {
            // Consume the OpenWeatherAPI in order to bring Forecast data in our page.
            List<GeoCodingResponse> coordinateResponse = _forecastRepository.GetCoordinates(city);

            List<WeatherResponse> weatherResponse = _forecastRepository.GetForecast(coordinateResponse);

            List<City> viewModel = new List<City>();

            if (weatherResponse != null)
            {
                for(int i = 0; i < weatherResponse.Count; i++)
                {
                    City modelData = new City();

                    modelData.Country = weatherResponse[i].sys.country;
                    modelData.Name = weatherResponse[i].name;
                    modelData.Humidity = weatherResponse[i].main.humidity;
                    modelData.Pressure = weatherResponse[i].main.pressure;
                    modelData.Temp = weatherResponse[i].main.temp;
                    modelData.Weather = weatherResponse[i].weather[0].main;
                    modelData.Wind = weatherResponse[i].wind.speed;

                    viewModel.Add(modelData);
                }
            }
            return View(viewModel);
        }
    }
}
