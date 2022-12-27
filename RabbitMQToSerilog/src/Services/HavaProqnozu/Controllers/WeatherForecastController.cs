using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HavaProqnozu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEventBus _eventBus;
        public WeatherForecastController(IEventBus eventBus, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

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

            string json = JsonSerializer.Serialize( data, (JsonSerializerOptions)null);
            _eventBus.Publish(new HavaIntegrationEvent { JsonData = json });

            return data;
        }
    }
}