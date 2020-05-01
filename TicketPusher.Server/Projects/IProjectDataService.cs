using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public interface IProjectDataService : IEntityDataService<ProjectDto, CreateProjectDto>
    {
    }
}