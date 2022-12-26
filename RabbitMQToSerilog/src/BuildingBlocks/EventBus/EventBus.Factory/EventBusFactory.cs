using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return config.EventBusType switch
            {
                //EventBusType.AzureServiceBus => new EventBusServiceBus(config, serviceProvider), eger azure da olsaydi secim ola bilerdi
                _ => new EventBusRabbitMQ(serviceProvider, config),
            };
        }
    }
}
