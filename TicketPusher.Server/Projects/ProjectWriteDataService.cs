using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectWriteDataService : EntityWriteDataService<ProjectDto, CreateProjectDto>, IProjectWriteDataService
    {

        public ProjectWriteDataService(HttpClient httpClient) : base(httpClient, "projects")
        {
        }

        public async Task<EnvelopeDto<ProjectDto>> UpdateProjectAsync(Guid projectId, UpdateProjectDto project)
        {
            var data = await HttpClient.PutJsonAsync<EnvelopeDto<ProjectDto>>($"{ApiBaseUri}/{projectId}", project);
            return data;
        }

    }
}