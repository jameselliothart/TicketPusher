using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public interface IProjectWriteDataService : IEntityWriteDataService<ProjectDto, CreateProjectDto>
    {
    }
}