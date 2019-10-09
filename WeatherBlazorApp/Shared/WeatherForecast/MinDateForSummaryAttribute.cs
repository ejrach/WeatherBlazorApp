using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace WeatherBlazorApp.Shared.WeatherForecast
{
    /// <summary>
    /// This attribute should only be used on WeatherForecast.Summary
    /// </summary>
    public class MinDateForSummaryAttribute : ValidationAttribute
    {
        public MinDateForSummaryAttribute() : base("Weather forecasts before 01-01-1900 must not contain a summary") { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo datePropertyInfo = validationContext.ObjectType.GetProperty(nameof(WeatherForecast.Date));
            if (validationContext.MemberName != nameof(WeatherForecast.Summary) || validationContext.ObjectType != typeof(WeatherForecast))
            {
                return new ValidationResult("This attribute should only be used on WeatherForecast.Summary");
            }
            var datePropertyValue = (DateTime)datePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var summaryPropertyValue = value as string;
            var minDateForSummary = new DateTime(1900, 1, 1);
            if (datePropertyValue < minDateForSummary && !String.IsNullOrWhiteSpace(summaryPropertyValue))
            {
                return new ValidationResult(ErrorMessageString);
            }
            return null;
        }
    }
}
