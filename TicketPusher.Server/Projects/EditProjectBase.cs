using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class EditProjectBase : EditEntityBase<ProjectDto, CreateProjectDto, IProjectDataService>
    {
        protected override string GetEntityIdentifier(EnvelopeDto<ProjectDto> envelope) =>
            envelope.Result.Id.ToString();
    }
}