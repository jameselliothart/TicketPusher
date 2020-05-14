using System;
using Blazored.Modal;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace TicketPusher.Server.Tickets
{
    public class CloseTicketBase : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        [Inject]
        protected IToastService ToastService { get; set; }

        [Inject]
        protected ITicketWriteDataService TicketWriteDataService { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        protected CloseTicketDto CloseTicketModel { get; set; } =
            new CloseTicketDto() { Resolution = string.Empty };

        protected async void HandleValidSubmit()
        {
            var completedTicket = await TicketWriteDataService.CloseTicketAsync(Id, CloseTicketModel);
            ToastService.ShowSuccess($"Closed ticket {Id}", "Success!");

            BlazoredModal.Close();
        }
    }
}