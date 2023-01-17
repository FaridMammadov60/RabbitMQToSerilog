using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System.Web.Http;
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
                 .WriteTo.Http(requestUri: "http://localhost:5181", queueLimitBytes: null,
            textFormatter: new JsonFormatter())
                 .MinimumLevel.Warning()
                 .CreateLogger();


           // Log.Logger.Warning("test");

        }
    }
}
