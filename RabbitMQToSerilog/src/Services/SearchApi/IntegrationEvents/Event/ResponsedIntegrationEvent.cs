using EventBus.Base.Event;

namespace SearchApi.IntegrationEvents.Event
{
    public class ResponsedIntegrationEvent : IntegrationEvent
    {
        public string JsonData { get; set; }

    }
}
