using EventBus.Base.Event;

namespace HavaProqnozu
{
    public class HavaIntegrationEvent:IntegrationEvent
    {
        public string JsonData { get; set; }
    }
}
