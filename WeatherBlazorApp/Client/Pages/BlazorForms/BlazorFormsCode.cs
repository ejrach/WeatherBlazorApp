using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using WeatherBlazorApp.Shared.WeatherForecast;

namespace WeatherBlazorApp.Client.Pages.BlazorForms
{
    public class BlazorFormsCode : ComponentBase
    {
        public EditContext EditContext;
        public WeatherForecast Model;

        [Inject]
        HttpClient Http { get; set; }

        //[Inject]
        //IUriHelper UriHelper { get; set; }

        [Inject]
        NavigationManager UriHelper { get; set; }

        public bool saving = false;

        public BlazorFormsCode()
        {
            Model = new WeatherForecast()
            {
                Date = DateTime.Now,
                Summary = "Test",
                TemperatureC = 21
            };
            EditContext = new EditContext(Model);
            EditContext.OnFieldChanged += EditContext_OnFieldChanged;
        }

        private void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            Console.WriteLine($"EditContext_OnFieldChanged - {e.FieldIdentifier.FieldName}");
            if (e.FieldIdentifier.FieldName == nameof(Model.Date))
            {
                EditContext.NotifyFieldChanged(EditContext.Field(nameof(Model.Summary)));
            }
        }

        protected async void HandleSubmit()
        {
            if (!EditContext.GetValidationMessages().Any())
            {
                saving = true;
                Task postTask = Http.SendJsonAsync(HttpMethod.Post, "weatherforecast", Model);
                await postTask.ContinueWith(task => {
                    Console.WriteLine($"task.IsFaulted: {task.IsFaulted}");
                    if (task.IsFaulted)
                    {
                        Console.Error.WriteLine(task.Exception.ToString());
                        saving = false;
                    }
                    else
                    {
                        UriHelper.NavigateTo("fetchdata");
                    }
                });
            }
        }
    }
}
