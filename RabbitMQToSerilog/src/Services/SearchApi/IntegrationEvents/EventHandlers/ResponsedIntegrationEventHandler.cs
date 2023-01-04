using EventBus.Base.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchApi.IntegrationEvents.Event;

namespace SearchApi.IntegrationEvents.EventHandlers
{
    public class ResponsedIntegrationEventHandler : IIntegrationEventHandler<ResponsedIntegrationEvent>
    {
        private readonly ILogger<ResponsedIntegrationEventHandler> _logger;

        public ResponsedIntegrationEventHandler(ILogger<ResponsedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ResponsedIntegrationEvent @event)
        {
            var data = (JObject)JsonConvert.DeserializeObject(@event.JsonData);
            switch (data["Level"].Value<string>() ?? "Debug")
            {
                case "Trace":
                    _logger.LogTrace(@event.JsonData);
                    break;
                case "Debug":
                    _logger.LogDebug(@event.JsonData);
                    break;
                case "Information":
                    _logger.LogInformation(@event.JsonData);
                    break;
                case "Warning":
                    _logger.LogWarning(@event.JsonData);
                    break;
                case "Fatal":
                    _logger.LogCritical(@event.JsonData);
                    break;
                case "Error":
                    _logger.LogError(@event.JsonData);
                    break;
            }            

            return Task.CompletedTask;
        }
    }
}
