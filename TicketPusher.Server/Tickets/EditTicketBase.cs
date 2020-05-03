using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
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
    }
}