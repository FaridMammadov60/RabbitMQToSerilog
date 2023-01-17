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
            _logger = logger;
        }

        [HttpGet("Get2")]
        public async Task<IActionResult> TestGet2()
        {
            _logger.LogInformation("oooooooley");
            return Ok("Alindi");
        }

        [HttpPost]
        public async Task<IActionResult> TestPost()
        {

            string data1 = "Testttttttttttttttttttttttt";
            string data2 = "Testttttttttttttttttttttttt";
            string json = JsonSerializer.Serialize(new { Data = data1, Test = data2 }, (JsonSerializerOptions)null);

            //Log.Logger.Information(json);
            _logger.LogWarning(json);


            return Ok(json);
        }

        [HttpPost("TestPost2")]
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





    }
}
