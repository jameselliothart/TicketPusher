using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Tickets;
using TicketPusher.Server.Projects;
using TicketPusher.Server.Shared;
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

        public async Task AddTicket(List<ProjectDto> projects)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditTicket.Projects), projects);
            var formModal = _modal.Show<EditTicket>("Add Ticket", parameters);
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
        }

        protected string DecodeProjectId(Guid projectId) =>
            Projects.Find(p => p.Id == projectId).Name;

    }
}