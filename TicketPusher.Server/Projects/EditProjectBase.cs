using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class EditProjectBase : EditEntityBase<ProjectDto, CreateProjectDto, IProjectWriteDataService>
    {
        public bool DialogIsOpen { get; set; } = false;

        [Parameter]
        public Func<Task> OnSubmitProject { get; set; }

        [Parameter]
        public List<ProjectDto> Projects { get; set; }

        [Parameter]
        public RenderFragment Button { get; set; }

        protected override string GetSuccessMessage(EnvelopeDto<ProjectDto> envelope) =>
            $"Added project {envelope.Result.Id.ToString()}";

        protected void OpenDialog()
        {
            EntityModel = new CreateProjectDto() { _parentProjectIdAsString = Project.None.Id.ToString() };
            DialogIsOpen = true;
        }

        protected async void SubmitProject()
        {
            var addedEntity = await EntityDataService.CreateEntityAsync(EntityModel);
            await OnSubmitProject?.Invoke();
            DialogIsOpen = false;
            StateHasChanged();
        }

    }
}