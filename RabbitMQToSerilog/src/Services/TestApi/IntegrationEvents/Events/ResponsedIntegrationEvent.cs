using EventBus.Base.Event;

namespace TestApi.IntegrationEvents.Events
{
    public class ResponsedIntegrationEvent:IntegrationEvent
    {
        public string JsonData { get; set; }
    }
}
