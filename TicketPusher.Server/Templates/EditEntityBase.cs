using Microsoft.AspNetCore.Components;
using TicketPusher.Server.Shared;

namespace TicketPusher.Server.Templates
{
    public abstract class EditEntityBase<TDto, TCreateDto, TDataService> : ComponentBase
        where TCreateDto : new()
        where TDataService : IEntityWriteDataService<TDto, TCreateDto>
    {
        [Parameter]
        public TDto ParentEntity { get; set; }

        [Inject]
        protected TDataService EntityDataService { get; set; }

        protected TCreateDto EntityModel { get; set;} = new TCreateDto();

        protected async void HandleValidSubmit()
        {
            var addedEntity = await EntityDataService.CreateEntityAsync(EntityModel);
            var successMessage = GetSuccessMessage(addedEntity);
        }

        protected abstract string GetSuccessMessage(EnvelopeDto<TDto> envelope);
    }
}