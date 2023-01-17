using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;

namespace RequestResponseMiddleware
{
    public class RequestResponseMiddlewareLib
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddlewareLib> _logger;

        public RequestResponseMiddlewareLib(RequestDelegate next, ILogger<RequestResponseMiddlewareLib> logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)        
        {
            //_logger.LogWarning($"{context.Request.QueryString}");           

            var respons = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();

            var data = (JObject)JsonConvert.DeserializeObject(respons);


            switch (data["Level"].Value<string>() ?? "Debug")
            {
                case "Trace":
                    _logger.LogTrace(respons);
                    break;
                case "Debug":
                    _logger.LogDebug(respons);
                    break;
                case "Information":
                    _logger.LogInformation(respons);
                    break;
                case "Warning":
                    _logger.LogWarning(respons);
                    break;
                case "Fatal":
                    _logger.LogCritical(respons);
                    break;
                case "Error":
                    _logger.LogError(respons);
                    break;
            }

            await next.Invoke(context);
        }
    }
}
