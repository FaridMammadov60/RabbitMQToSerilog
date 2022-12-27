using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using TestApi.AutoFac;
using TestApi.IntegrationEvents.EventHandlers;
using TestApi.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModule());
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TestIntegrationEventHandlers>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sp =>
{
    EventBusConfig config = new EventBusConfig
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "TestApi",
        EventBusType = EventBusType.RabbitMQ,
        //Connection = new ConnectionFactory()
        //{
        //    HostName = "rabbitmq"
        //}
    };

    return EventBusFactory.Create(config, sp);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
//eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandlers>();

app.Run();
