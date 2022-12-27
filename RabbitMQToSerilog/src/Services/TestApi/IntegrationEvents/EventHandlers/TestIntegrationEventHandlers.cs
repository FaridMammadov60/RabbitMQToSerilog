using EventBus.Base.Abstraction;
using TestApi.IntegrationEvents.Events;

namespace TestApi.IntegrationEvents.EventHandlers
{
    public class TestIntegrationEventHandlers : IIntegrationEventHandler<TestIntegrationEvent>
    {
        private readonly ILogger<TestIntegrationEventHandlers> _logger;

        public TestIntegrationEventHandlers(ILogger<TestIntegrationEventHandlers> logger)
        {
            _logger = logger;
        }

        public Task Handle(TestIntegrationEvent @event)
        {
            _logger.LogInformation($"{@event.Name}- {@event.Id} : {@event.CreatedDate}");

            //Log.Information($"{@event.Name}- {@event.Id} : {@event.CreatedDate}"); sadece morterizedeki datalari loga yazdirmaq ucun
            return Task.CompletedTask;
        }
    }
}
