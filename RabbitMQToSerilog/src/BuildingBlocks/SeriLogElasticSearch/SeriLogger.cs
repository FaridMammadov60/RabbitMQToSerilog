using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriLogElasticSearch
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
        {
            var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
            var elasticUser = context.Configuration.GetValue<string>("ElasticConfiguration:User");
            var elasticPassword = context.Configuration.GetValue<string>("ElasticConfiguration:Password");

            configuration
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    ModifyConnectionSettings = x =>x.BasicAuthentication(elasticUser, elasticPassword)
                    .ServerCertificateValidationCallback((o, certificate, chain, errors) => true)
                    .ServerCertificateValidationCallback(CertificateValidations.AllowAll),
                    IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".","-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM-dd}",
                    TypeName = null,
                    AutoRegisterTemplate= true,
                    NumberOfShards= 2,
                    NumberOfReplicas= 1,
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
                    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information

                })
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
            .ReadFrom.Configuration(context.Configuration);

        };
    }
}
