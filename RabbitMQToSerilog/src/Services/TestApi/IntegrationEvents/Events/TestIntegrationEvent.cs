using EventBus.Base.Event;

namespace TestApi.IntegrationEvents.Events
{
    public class TestIntegrationEvent : IntegrationEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

