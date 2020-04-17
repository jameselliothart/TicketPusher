using System;
using TicketPusher.Domain.Common;

namespace TicketPusher.Domain.CompletedTickets
{
    public class CompletedTicket : Entity
    {
        public TicketDetails TicketDetails { get; private set; }

        private CompletedTicket() {}

        public CompletedTicket(TicketDetails ticketDetails)
        {
            TicketDetails = ticketDetails ?? throw new ArgumentNullException(nameof(ticketDetails));
        }

    }
}