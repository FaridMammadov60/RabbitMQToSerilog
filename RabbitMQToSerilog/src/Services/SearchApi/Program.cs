using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using SearchApi.AutoFac;
using SearchApi.IntegrationEvents.Event;
using SearchApi.IntegrationEvents.EventHandlers;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System.Configuration;
using System.Reflection;
using System;
using Serilog.Exceptions;




var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModule());
    });

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile(
                   $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                   optional: true)
               .Build();


var logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .Enrich.WithExceptionDetails()
              .Enrich.WithMachineName()
              .WriteTo.Debug()
              .WriteTo.Console()
              .WriteTo.File("logs.txt")
              .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["Elasticsearch:Uri"]))
              {
                  CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                  ModifyConnectionSettings = c => c.ServerCertificateValidationCallback(
                      (o, certificate, arg3, arg4) => { return true; }),
                  AutoRegisterTemplate = true,
                  IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
              })
              .Enrich.WithProperty("Environment", environment)
              .ReadFrom.Configuration(configuration)
              .CreateLogger();

builder.Logging.AddSerilog(logger);

//var logger = new LoggerConfiguration()
//                .MinimumLevel.Warning()
//                .WriteTo.File("logs.txt")
//                .WriteTo.Elasticsearch()
//                .CreateLogger();
//builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddLogging(i => 
//{ 
//    i.ClearProviders();
//    i.AddProvider(new MyCustomLoggerProvide());
//    });
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MedicalIntegrationEventHandler>();
builder.Services.AddScoped<ResponsedIntegrationEventHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sp =>
{
    EventBusConfig config = new EventBusConfig
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "SearchApi",
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


IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<MedicalIntegrationEvent, MedicalIntegrationEventHandler>();
eventBus.Subscribe<ResponsedIntegrationEvent, ResponsedIntegrationEventHandler>();

app.Run();
