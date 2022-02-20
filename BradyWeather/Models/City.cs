using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BradyWeather.Models
{
    public class City
    {
        [Display(Name = "City:")]
        public string Name { get; set; }

        [Display(Name = "Country:")]
        public string Country { get; set; }

        [Display(Name = "Temperature:")]
        public double Temp { get; set; }

        [Display(Name = "Humidity")]
        public int Humidity { get; set; }

        [Display(Name = "Pressure:")]
        public int Pressure { get; set; }

        [Display(Name = "Wind Speed:")]
        public double Wind { get; set; }

        [Display(Name = "Weather Condition:")]
        public string Weather { get; set; }

    }
}
