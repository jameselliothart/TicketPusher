using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Projects
{
    public class EditProjectBase : EditEntityBase<ProjectDto, CreateProjectDto, IProjectWriteDataService>
    {
        public bool DialogIsOpen { get; set; } = false;

        [Parameter]
        public Func<Task> OnSubmitProject { get; set; }

        protected override string GetSuccessMessage(EnvelopeDto<ProjectDto> envelope) =>
            $"Added project {envelope.Result.Id.ToString()}";

        protected void OpenDialog()
        {
            EntityModel = new CreateProjectDto();
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