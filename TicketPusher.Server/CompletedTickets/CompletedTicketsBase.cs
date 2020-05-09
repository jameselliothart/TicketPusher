using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.CompletedTickets;
using TicketPusher.API.Projects;
using TicketPusher.Server.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.CompletedTickets
{
    public class CompletedTicketsBase : EntityBase<CompletedTicketDto, ICompletedTicketReadDataService>
    {
        protected List<ProjectDto> Projects;

        [Inject]
        private IProjectReadDataService _projectReadDataService { get; set; }

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
            Projects = (await _projectReadDataService.GetEntityListAsync()).Result;
        }
    }
}