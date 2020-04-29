using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;

namespace TicketPusher.Server.Projects
{
    public class ProjectsBase : ComponentBase
    {
        [Inject]
        private IProjectDataService projectDataService { get; set; }

        public List<ProjectDto> projects;

        protected override async Task OnInitializedAsync()
        {
            projects = await RefreshData();
        }

        protected async Task<List<ProjectDto>> RefreshData()
        {
            var data = await projectDataService.GetProjectListAsync();
            return data.Result;
        }
    }
}