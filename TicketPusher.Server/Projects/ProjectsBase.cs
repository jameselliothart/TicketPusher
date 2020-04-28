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
        protected IProjectDataService projectDataService { get; set; }
        [Inject]
        protected IToastService _toastService { get; set; }

        // private List<ProjectDto> _projects = new List<ProjectDto>();
        public List<ProjectDto> projects;

        protected CreateProjectDto ProjectModel = new CreateProjectDto();

        protected override async Task OnInitializedAsync()
        {
            await RefreshData();
        }

        protected async Task RefreshData()
        {
            var data = await projectDataService.GetProjectListAsync();
            projects = data.Result;
        }
    }
}