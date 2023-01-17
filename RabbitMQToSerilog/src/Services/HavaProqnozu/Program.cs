using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Base;
using EventBus.Factory;
using HavaProqnozu.AutoFac;
using Serilog.Formatting.Json;
using Serilog;
using ILogger = Serilog.ILogger;

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

builder.Services.AddHttpContextAccessor();

ILogger log = new LoggerConfiguration()
               .Enrich.FromLogContext()
         .WriteTo.Http(requestUri: "http://localhost:5181/api/testApi", queueLimitBytes: null,
          textFormatter: new JsonFormatter())      

         .MinimumLevel.Information()
         .CreateLogger();

//log.Information("sdsdsdsdds");

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

app.Run();
