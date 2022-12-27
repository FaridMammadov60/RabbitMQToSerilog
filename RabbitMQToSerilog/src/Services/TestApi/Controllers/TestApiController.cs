using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestApi.IntegrationEvents.Events;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {


        private readonly IEventBus _eventBus;

        public TestApiController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }


        [HttpPost]
        public async Task<IActionResult> GetAll(int id, string data)
        {
            _eventBus.Publish(new MedicalIntegrationEvent { Name = data, Id = id });
           
            return Ok("Ok");
        }


        [HttpPost("tester")]
        public async Task<IActionResult> TestPost(string data1, string data2, double eded, long zad, bool exist)
        {
            string json = JsonSerializer.Serialize(new { Data = data1, Test = data2, Eded = eded, Zad = zad, Bol = exist }, (JsonSerializerOptions)null);

            _eventBus.Publish(new ResponsedIntegrationEvent { JsonData = json });

            return Ok(json);
        }

    }
}
