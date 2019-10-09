using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WeatherBlazorApp.Shared.WeatherForecast;
using Newtonsoft.Json;

namespace WeatherBlazorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly string dataPath;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWebHostEnvironment hostingEnvironment)
        {
            this.logger = logger;
            dataPath = Path.Combine(hostingEnvironment.ContentRootPath, "Data", "data.json");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string json;
            using (StreamReader reader = new StreamReader(dataPath, true))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<WeatherForecast[]>(json);
        }

        [HttpPost]
        public IEnumerable<WeatherForecast> Post(WeatherForecast model)
        {
            string json;
            using (StreamReader reader = new StreamReader(dataPath, true))
            {
                json = reader.ReadToEnd();
            }
            var data = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);
            data.Add(model);
            json = JsonConvert.SerializeObject(data.ToArray());
            using (StreamWriter writer = new StreamWriter(dataPath, false))
            {
                writer.Write(json);
            }
            return data;
        }
    }
}