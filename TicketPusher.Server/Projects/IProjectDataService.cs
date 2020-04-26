using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Utils;
using TicketPusher.Server.Shared;

namespace TicketPusher.Server.Projects
{
    public interface IProjectDataService
    {
        Task<EnvelopeDto<ProjectDto>> CreateProjectAsync(CreateProjectDto project);
        Task<EnvelopeDto<List<ProjectDto>>> GetProjectListAsync();
    }
}