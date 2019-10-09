using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WeatherBlazorApp.Shared.WeatherForecast
{
    public class WeatherForecast
    {
        // This is necessary in order for DateTime validation to function
        protected DateTimeConverter converter = new DateTimeConverter();

        [Range(typeof(DateTime),"1753-01-01","3000-01-01",ErrorMessage = "Date must be between 01-01-1753 and 01-01-3000.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Temperature deg C is required.")]
        public int? TemperatureC { get; set; }

        public int? TemperatureF => TemperatureC.HasValue ? (32 + (int?)(TemperatureC / 0.5556)) : null;

        [StringLength(25, ErrorMessage = "Summary must be less than 25 characters.")]
        [MinDateForSummary] //custom attribute
        public string Summary { get; set; }
    }
}
