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

namespace TicketPusher.Server.Projects
{
    public class ProjectDataService : IProjectDataService
    {
        private readonly HttpClient _httpClient;

        public ProjectDataService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<EnvelopeDto<List<ProjectDto>>> GetEntityListAsync()
        {
            var data = await _httpClient.GetJsonAsync<EnvelopeDto<List<ProjectDto>>>("projects");
            return data;
        }

        public async Task<EnvelopeDto<ProjectDto>> CreateEntityAsync(CreateProjectDto project)
        {
            var data = await _httpClient.PostJsonAsync<EnvelopeDto<ProjectDto>>("projects", project);
            return data;
        }
    }
}