using System.Threading.Tasks;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : EntityBase<ProjectDto, IProjectReadDataService>
    {
        [Inject]
        private IModalService _modal { get; set; }

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
            StateHasChanged();
        }
    }
}