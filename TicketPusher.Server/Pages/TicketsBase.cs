using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPusher.API.Tickets;
using TicketPusher.Server.Data;

namespace TicketPusher.Server.Pages
{
    public class TicketsBase : ComponentBase
    {
        [Inject] private TicketService ticketService { get; set; }

        private List<TicketDto> _tickets = new List<TicketDto>();
        public IReadOnlyList<TicketDto> tickets => _tickets.ToList();

        protected override async Task OnInitializedAsync()
        {
            var data = await ticketService.GetTicketsAsync();
            _tickets.AddRange(data);
        }
    }
}
