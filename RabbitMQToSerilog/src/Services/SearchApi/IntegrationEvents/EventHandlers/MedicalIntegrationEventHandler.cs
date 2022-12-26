using EventBus.Base.Abstraction;
using SearchApi.IntegrationEvents.Event;
using Serilog;
using System.Configuration.Internal;

namespace SearchApi.IntegrationEvents.EventHandlers
{
    public class MedicalIntegrationEventHandler : IIntegrationEventHandler<MedicalIntegrationEvent>
    {
        private readonly ILogger<MedicalIntegrationEventHandler> _logger;

        public MedicalIntegrationEventHandler(ILogger<MedicalIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(MedicalIntegrationEvent @event)
        {
            _logger.LogInformation($"{@event.Name}- {@event.Id} : {@event.CreatedDate}");

            //Log.Information($"{@event.Name}- {@event.Id} : {@event.CreatedDate}"); sadece morterizedeki datalari loga yazdirmaq ucun
            return Task.CompletedTask;
        }
    }
}
