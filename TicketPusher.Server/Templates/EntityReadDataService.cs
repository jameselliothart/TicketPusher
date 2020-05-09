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
    public abstract class EntityReadDataService<TDto> : EntityDataService, IEntityReadDataService<TDto>
    {

        public EntityReadDataService(HttpClient httpClient, string apiBaseUri) : base(httpClient, apiBaseUri)
        {
        }

        public async Task<EnvelopeDto<List<TDto>>> GetEntityListAsync()
        {
            var data = await HttpClient.GetJsonAsync<EnvelopeDto<List<TDto>>>(ApiBaseUri);
            return data;
        }

    }
}