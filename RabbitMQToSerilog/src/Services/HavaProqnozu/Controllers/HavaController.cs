using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HavaProqnozu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HavaController : ControllerBase
    {
        private readonly ILogger<HavaController> _logger;

        public HavaController(ILogger<HavaController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> TestPost()
        {

            string data1 = "Testttttttttttttttttttttttt";
            string data2 = "Testttttttttttttttttttttttt";
            string json = JsonSerializer.Serialize(new { Data = data1, Test = data2 }, (JsonSerializerOptions)null);

            _logger.LogWarning(data1);
            _logger.LogWarning(json);


            return Ok(json);
        }
    }
}
