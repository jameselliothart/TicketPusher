using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class UpdateProjectBase : ComponentBase
    {
        public bool DialogIsOpen { get; set; } = false;

        [Parameter]
        public Func<Task> OnSubmitProject { get; set; }

        [Parameter]
        public List<ProjectDto> Projects { get; set; }

        [Parameter]
        public RenderFragment Button { get; set; }

        [Parameter]
        public ProjectDto Project { get; set; }

        [Inject]
        protected IProjectWriteDataService EntityDataService { get; set; }

        protected UpdateProjectDto EntityModel { get; set;} = new UpdateProjectDto();

        protected void OpenDialog(ProjectDto project)
        {
            EntityModel = new UpdateProjectDto()
            {
                Name = project.Name,
                ParentProjectId = project.ParentProjectId
            };
            DialogIsOpen = true;
        }

        protected async void SubmitProject(Guid projectId)
        {
            var addedEntity = await EntityDataService.UpdateProjectAsync(projectId, EntityModel);
            await OnSubmitProject?.Invoke();
            DialogIsOpen = false;
            StateHasChanged();
        }

    }
}