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

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
            Projects = (await _projectReadDataService.GetEntityListAsync()).Result;
            StateHasChanged();
        }

    }
}