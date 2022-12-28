using EventBus.Base.Event;

namespace HavaProqnozu
{
    public class ResponsedntegrationEvent : IntegrationEvent
    {
        public string JsonData { get; set; }
    }
}
