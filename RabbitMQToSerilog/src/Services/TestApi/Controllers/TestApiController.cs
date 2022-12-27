using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
