using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPusher.Server.Data;

namespace TicketPusher.Server.Pages
{
    public class TicketsBase : ComponentBase
    {
        [Inject] private TicketService ticketService { get; set; }
        public Ticket[] tickets;

        protected override async Task OnInitializedAsync()
        {
            tickets = await ticketService.GetTicketsAsync();
        }
    }
}
