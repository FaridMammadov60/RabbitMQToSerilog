using EventBus.Base.Event;

namespace TestApi.IntegrationEvents.Events
{
    public class MedicalIntegrationEvent : IntegrationEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
