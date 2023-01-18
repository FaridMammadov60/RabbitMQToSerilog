using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace _4._6Lognet4WebApplication
{
    public class FooAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5228");
                client.PostAsJsonAsync<string>(client.BaseAddress, loggingEvent.ToString());
            }
        }

    }
}