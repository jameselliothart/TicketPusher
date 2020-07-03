using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Server.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class TicketsBase : EntityBase<TicketDto, ITicketReadDataService>
    {
        protected List<ProjectDto> Projects;

        [Inject]
        private IProjectReadDataService _projectReadDataService { get; set; }

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
            Projects = (await _projectReadDataService.GetEntityListAsync()).Result;
            StateHasChanged();
        }

    }
}