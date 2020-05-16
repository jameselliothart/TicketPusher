using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.Server.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class TicketsBase : EntityBase<TicketDto, ITicketReadDataService>
    {
        protected List<ProjectDto> Projects;

        [Inject]
        private IProjectReadDataService _projectReadDataService { get; set; }

        [Inject]
        private IModalService _modal { get; set; }

        public async Task Close(Guid ticketId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(CloseTicket.Id), ticketId);
            var formModal = _modal.Show<CloseTicket>("Close Ticket", parameters);
            var result = await formModal.Result;
            if (!result.Cancelled)
            {
                await RefreshData();
            }
        }

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
            Projects = (await _projectReadDataService.GetEntityListAsync()).Result;
            StateHasChanged();
        }

    }
}