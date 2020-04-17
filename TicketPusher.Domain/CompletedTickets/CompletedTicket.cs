using System;
using TicketPusher.Domain.Common;

namespace TicketPusher.Domain.CompletedTickets
{
    public class CompletedTicket : Entity
    {
        public string Owner { get; private set; }
        public TicketDetails TicketDetails { get; private set; }
        public CompletedDetails CompletedDetails { get; private set; }

        private CompletedTicket() {}

        public CompletedTicket(string owner, TicketDetails ticketDetails, CompletedDetails completedDetails)
        {
            CompletedDetails = completedDetails ?? throw new ArgumentNullException(nameof(completedDetails));
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            TicketDetails = ticketDetails ?? throw new ArgumentNullException(nameof(ticketDetails));
        }

    }
}