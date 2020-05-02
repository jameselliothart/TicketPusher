using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Utils;
using TicketPusher.Server.Shared;

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