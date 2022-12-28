using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using SearchApi.IntegrationEvents.Event;

namespace SearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public SearchController(IEventBus eventBus)
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
