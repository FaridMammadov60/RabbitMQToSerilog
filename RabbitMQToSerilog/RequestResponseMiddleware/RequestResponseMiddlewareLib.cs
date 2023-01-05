using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware
{
    public class RequestResponseMiddlewareLib
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddlewareLib> logger;

        public RequestResponseMiddlewareLib(RequestDelegate next, ILogger<RequestResponseMiddlewareLib> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            logger.LogWarning($"{context.Request.QueryString}");
            await next.Invoke(context);
        }
    }
}
