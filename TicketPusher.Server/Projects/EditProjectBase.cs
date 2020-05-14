using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class EditProjectBase : EditEntityBase<ProjectDto, CreateProjectDto, IProjectWriteDataService>
    {
        protected override string GetSuccessMessage(EnvelopeDto<ProjectDto> envelope) =>
            $"Added project {envelope.Result.Id.ToString()}";
    }
}