using System;
using System.Net.Http;

namespace TicketPusher.Server.Templates
{
    public abstract class EntityDataService
    {
        protected readonly HttpClient HttpClient;
        protected readonly string ApiBaseUri;

        public EntityDataService(HttpClient httpClient, string apiBaseUri)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            ApiBaseUri = apiBaseUri ?? throw new ArgumentNullException(nameof(apiBaseUri));
        }

    }
}