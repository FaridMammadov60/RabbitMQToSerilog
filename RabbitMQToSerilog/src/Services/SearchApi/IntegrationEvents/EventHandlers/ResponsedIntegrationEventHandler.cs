using EventBus.Base.Abstraction;
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
            //Log.Information($"{@event.JsonData}");
            _logger.LogWarning(@event.JsonData);

            return Task.CompletedTask;
        }
    }
}
