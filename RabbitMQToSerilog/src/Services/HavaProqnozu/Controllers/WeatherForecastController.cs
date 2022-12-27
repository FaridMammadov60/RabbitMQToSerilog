using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HavaProqnozu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {        
               
        private readonly IEventBus _eventBus;
        public WeatherForecastController(IEventBus eventBus, ILogger<WeatherForecastController> logger)
        {           
            _eventBus = eventBus;
        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var data= Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            string json = JsonSerializer.Serialize( new { Data = data }, (JsonSerializerOptions)null);
            _eventBus.Publish(new ResponsedntegrationEvent { JsonData = json });

            return data;
        }
    }
}