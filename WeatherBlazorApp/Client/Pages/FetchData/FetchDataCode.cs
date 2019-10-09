using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using WeatherBlazorApp.Shared.WeatherForecast;

namespace WeatherBlazorApp.Client.Pages.FetchData
{
    public class FetchDataCode : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        public WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await Http.GetJsonAsync<WeatherForecast[]>("weatherforecast");
        }
    }
}
