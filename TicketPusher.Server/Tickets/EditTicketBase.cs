using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.Server.Projects;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class EditTicketBase : EditEntityBase<TicketDto, SubmitTicketDto, ITicketWriteDataService>
    {
        [Parameter]
        public List<ProjectDto> Projects { get; set; }

        protected override string GetSuccessMessage(EnvelopeDto<TicketDto> envelope) =>
            $"Added ticket {envelope.Result.Id.ToString()}";

        protected override Task OnInitializedAsync()
        {
            // TODO: get owner from dropdown of registered users
            EntityModel = new SubmitTicketDto() { Owner = "Unassigned" };
            return Task.CompletedTask;
        }
    }
}