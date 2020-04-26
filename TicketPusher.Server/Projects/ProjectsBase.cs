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
        [Inject]
        private IToastService _toastService { get; set; }

        private List<ProjectDto> _projects = new List<ProjectDto>();
        public IReadOnlyList<ProjectDto> projects => _projects.ToList();

        protected CreateProjectDto ProjectModel = new CreateProjectDto();

        protected async void HandleValidSubmit()
        {
            var data = await projectDataService.CreateProjectAsync(ProjectModel);
            _toastService.ShowSuccess($"Added project {data.Result.Id}", "Success!");
        }

        protected override async Task OnInitializedAsync()
        {
            var data = await projectDataService.GetProjectListAsync();
            _projects.AddRange(data.Result);
        }
    }
}