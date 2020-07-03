using System;
using System.Threading.Tasks;
using TicketPusher.DataTransfer;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public interface IProjectWriteDataService : IEntityWriteDataService<ProjectDto, CreateProjectDto>
    {
        Task<EnvelopeDto<ProjectDto>> UpdateProjectAsync(Guid projectId, UpdateProjectDto project);
    }
}