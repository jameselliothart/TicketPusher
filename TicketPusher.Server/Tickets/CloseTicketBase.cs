using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace TicketPusher.Server.Tickets
{
    public class CloseTicketBase : ComponentBase
    {

        [Inject]
        protected ITicketWriteDataService TicketWriteDataService { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public Func<Task> OnHandleValidSubmit { get; set; }
        public bool DialogIsOpen { get; set; } = false;

        protected CloseTicketDto CloseTicketModel { get; set; } =
            new CloseTicketDto() { Resolution = string.Empty };
        protected async void HandleValidSubmit()
        {
            var completedTicket = await TicketWriteDataService.CloseTicketAsync(Id, CloseTicketModel);
            await OnHandleValidSubmit?.Invoke();
            DialogIsOpen = false;
            StateHasChanged();
        }

        protected void OpenDialog()
        {
            DialogIsOpen = true;
        }
    }
}