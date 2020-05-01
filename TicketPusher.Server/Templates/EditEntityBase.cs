using Blazored.Modal;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using TicketPusher.Server.Shared;

namespace TicketPusher.Server.Templates
{
    public abstract class EditEntityBase<TDto, TCreateDto, TDataService> : ComponentBase
        where TCreateDto : new()
        where TDataService : IEntityDataService<TDto, TCreateDto>
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        [Parameter]
        public TDto ParentProjects { get; set; }

        [Inject]
        protected IToastService ToastService { get; set; }

        [Inject]
        protected TDataService ProjectDataService { get; set; }

        protected TCreateDto ProjectModel { get; set;} = new TCreateDto();

        protected async void HandleValidSubmit()
        {
            var addedProject = await ProjectDataService.CreateEntityAsync(ProjectModel);
            var identifier = GetEntityIdentifier(addedProject);
            ToastService.ShowSuccess($"Added project {identifier}", "Success!");

            BlazoredModal.Close();
        }

        protected abstract string GetEntityIdentifier(EnvelopeDto<TDto> envelope);
    }
}