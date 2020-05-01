using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : EntityBase<ProjectDto, IProjectDataService>
    {
        [Inject]
        private IModalService _modal { get; set; }

        protected override async Task<List<ProjectDto>> RetrieveData()
        {
            var data = await EntityDataService.GetEntityListAsync();
            return data.Result;
        }

        public async Task AddProject()
        {
            var formModal = _modal.Show<EditProject>("Add Project");
            var result = await formModal.Result;
            if (!result.Cancelled) {
                await RefreshData();
            }
        }

    }
}