using EventBus.Base.Event;

namespace SearchApi.IntegrationEvents.Event
{
    public class MedicalIntegrationEvent:IntegrationEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
