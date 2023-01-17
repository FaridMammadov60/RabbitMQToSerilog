using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestApi.IntegrationEvents.Events;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test46FrameworkController : ControllerBase
    {
        private readonly ILogger<ResponsedIntegrationEvent> _logger;

        public Test46FrameworkController(ILogger<ResponsedIntegrationEvent> logger)
        {
            _logger = logger;
        }


        [HttpPost("post2")]
        public async Task<IActionResult> TestPost2(string json)
        {
            _logger.LogInformation(json);
            return Ok("Alindi");
        }


        [HttpGet]
        public async Task<IActionResult> TestGet(string json)
        {
            _logger.LogInformation(json);
            return Ok("Alindi");
        }

        [HttpGet("Get2")]
        public async Task<IActionResult> TestGet2()
        {
            _logger.LogInformation("oooooooley");
            return Ok("Alindi");
        }

        [HttpPost]
        public async Task<IActionResult> Test46()
        {

            string data1 = "4666666666";
            string data2 = "4666666666";
            string json = JsonSerializer.Serialize(new { Data = data1, Test = data2 }, (JsonSerializerOptions)null);

            _logger.LogWarning(json);


            return Ok(json);
        }
    }
}
