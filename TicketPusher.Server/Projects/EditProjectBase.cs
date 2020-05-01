using Blazored.Modal;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;

namespace TicketPusher.Server.Projects
{
    public class EditProjectBase : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        [Parameter]
        public ProjectDto ParentProjects { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IProjectDataService _projectDataService { get; set; }

        protected CreateProjectDto ProjectModel { get; set;} = new CreateProjectDto();

        protected async void HandleValidSubmit()
        {
            var addedProject = await _projectDataService.CreateProjectAsync(ProjectModel);
            _toastService.ShowSuccess($"Added project {addedProject.Result.Id}", "Success!");

            BlazoredModal.Close();
        }
    }
}