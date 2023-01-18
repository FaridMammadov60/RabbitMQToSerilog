using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace _4._6Lognet4WebApplication
{
    public class FaridAppender : AppenderSkeleton
    {
        public FaridAppender()
        {
            Console.WriteLine("zad");
        }
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Do something with the logged data, like calling your web url
            PostEvent(loggingEvent).ConfigureAwait(false);
        }


        private async Task PostEvent(LoggingEvent loggingEvent)
        {
            using (var client = new HttpClient())
            {

                var content = loggingEvent.RenderedMessage;
                var response = await client.PostAsJsonAsync<string>("http://localhost:5228", content);

                var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();


                response.EnsureSuccessStatusCode();

            }
        }
    
    }
}