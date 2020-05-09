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
    public abstract class EntityWriteDataService<TDto, TCreateDto> : EntityDataService, IEntityWriteDataService<TDto, TCreateDto>
    {

        public EntityWriteDataService(HttpClient httpClient, string apiBaseUri) : base(httpClient, apiBaseUri)
        {
        }

        public async Task<EnvelopeDto<TDto>> CreateEntityAsync(TCreateDto project)
        {
            var data = await HttpClient.PostJsonAsync<EnvelopeDto<TDto>>(ApiBaseUri, project);
            return data;
        }
    }
}