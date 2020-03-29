using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPusher.Server.Data
{
    public class TicketService
    {
        private Ticket[] tickets = {new Ticket("James", "first ticket", DateTime.Now), new Ticket("Jennifer", "second ticket", DateTime.Now)};
        public Task<Ticket[]> GetTicketsAsync()
        {
            return Task.FromResult(tickets);
        }
    }
}
