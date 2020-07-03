using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class EditTicketBase : EditEntityBase<TicketDto, SubmitTicketDto, ITicketWriteDataService>
    {
        [Parameter]
        public List<ProjectDto> Projects { get; set; }

        public bool DialogIsOpen { get; set; } = false;

        [Parameter]
        public Func<Task> OnSubmitTicket { get; set; }

        protected override string GetSuccessMessage(EnvelopeDto<TicketDto> envelope) =>
            $"Added ticket {envelope.Result.Id.ToString()}";

        protected void OpenDialog()
        {
            // TODO: get owner from dropdown of registered users
            EntityModel = new SubmitTicketDto() { Owner = "Unassigned" };
            DialogIsOpen = true;
        }

        protected async void SubmitTicket()
        {
            var addedEntity = await EntityDataService.CreateEntityAsync(EntityModel);
            await OnSubmitTicket?.Invoke();
            DialogIsOpen = false;
            StateHasChanged();
        }
    }
}