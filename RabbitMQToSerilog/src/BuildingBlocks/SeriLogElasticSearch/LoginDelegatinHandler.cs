﻿using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace SeriLogElasticSearch
{
    public class LoginDelegatinHandler : DelegatingHandler
    {
        private readonly ILogger<LoginDelegatinHandler> logger;

        public LoginDelegatinHandler(ILogger<LoginDelegatinHandler> logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Sending request to {Url}", request.RequestUri);
                var response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation("Received a success response from {Url}", response.RequestMessage.RequestUri);
                }
                else
                {
                    logger.LogWarning("Received a non-success status code {StatusCode} from {Url}", (int)response.StatusCode, response.RequestMessage.RequestUri);
                }
                return response;
            }
            catch (HttpRequestException ex)
            when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionRefused)
            {
                var hostWithPort = request.RequestUri.IsDefaultPort
                    ? request.RequestUri.DnsSafeHost
                    : $"{request.RequestUri.DnsSafeHost}:{request.RequestUri.Port}";
                logger.LogCritical(ex, "", hostWithPort);

                return new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway)
                {
                    RequestMessage = request,
                };
            }
        }
    }
}
