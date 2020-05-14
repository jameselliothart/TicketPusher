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

        public async Task AddProject()
        {
            var formModal = _modal.Show<EditProject>("Add Project");
            var result = await formModal.Result;
            if (!result.Cancelled) {
                await RefreshData();
            }
        }

        protected override async Task RefreshData()
        {
            Entities = await RetrieveMainEntities();
        }
    }
}