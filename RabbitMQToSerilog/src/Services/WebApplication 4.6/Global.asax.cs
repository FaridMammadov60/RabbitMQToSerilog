using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.Http.TextFormatters;
using SerilogWeb.Classic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication_4._6
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);





            Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
           .WriteTo.Http(requestUri: "http://localhost:5181/api/testApi", queueLimitBytes: null,
            textFormatter: new JsonFormatter())
           .WriteTo.Http(requestUri: "http://localhost:5181/api/Test46Framework", queueLimitBytes: int.MaxValue,
           textFormatter: new CompactJsonFormatter())           
           .MinimumLevel.Information()
           .CreateLogger();
                      

           
        }
    }
}
