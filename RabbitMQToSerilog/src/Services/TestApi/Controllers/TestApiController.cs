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
        private readonly ILogger<ResponsedIntegrationEvent> _logger;

        

        public TestApiController(ILogger<ResponsedIntegrationEvent> logger)
        {
            _logger= logger;
        }


        [HttpPost]       
        public async Task<IActionResult> TestPost()
        {
            string data1 = "Test";
            string data2 = "Test";
            string json = JsonSerializer.Serialize(new { Data = data1, Test = data2 }, (JsonSerializerOptions)null);

            _logger.LogWarning(json);
            

            return Ok(json);
        }

    }
}
