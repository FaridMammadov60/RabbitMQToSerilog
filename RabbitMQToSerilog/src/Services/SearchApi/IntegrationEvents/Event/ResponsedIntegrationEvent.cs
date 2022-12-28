using EventBus.Base.Event;
using Serilog.Formatting.Json;

namespace SearchApi.IntegrationEvents.Event
{
    public class ResponsedIntegrationEvent : IntegrationEvent
    {
        public string JsonData { get; set; }        

    }
}
