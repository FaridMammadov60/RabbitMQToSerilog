using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware
{
    public class Log4netRequestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<Log4netRequestMiddleware> _logger;

        public Log4netRequestMiddleware(RequestDelegate next, ILogger<Log4netRequestMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {

            _logger.LogWarning($"{context.Request.QueryString}");
            
            var test = context.Request.Body.ToString();
            //var test1 = context.Request.Form;
            var test2 = context.Request.Headers.ToString();
            var test3 = context.Request.Host.ToString();
            var test4 = context.RequestServices.ToString();
            

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
