using Autofac;
using Autofac.Extensions.DependencyInjection;
using RequestResponseMiddleware;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using System.Configuration;
using TestApi.AutoFac;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModule());
    });


//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
//    {
//        clientConfiguration.Username = "guest"; //"dev-user"
//        clientConfiguration.Password = "guest"; //
//        clientConfiguration.Exchange = "AdrenalinEventBus";
//        clientConfiguration.ExchangeType = "direct";
//        clientConfiguration.DeliveryMode = RabbitMQDeliveryMode.Durable;
//        clientConfiguration.RouteKey = "Responsed";
//        clientConfiguration.Port = 5672; //32672        
//        clientConfiguration.Hostnames.Add("127.0.0.1"); //"10.168.32.20"

//        sinkConfiguration.TextFormatter = new JsonFormatter();
//    }).MinimumLevel.Warning()
//    .CreateLogger();



Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

var loggerFactory = new LoggerFactory();
loggerFactory.AddSerilog(Log.Logger);

builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestResponseMiddlewareLib>();

app.UseAuthorization();

app.MapControllers();

//IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
//eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandlers>();

app.Run();
