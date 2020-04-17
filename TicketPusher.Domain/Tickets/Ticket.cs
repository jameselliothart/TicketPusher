using System;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.CompletedTickets;

namespace TicketPusher.Domain.Tickets
{
    public class Ticket : Entity
    {
        public string Owner { get; private set; }
        public TicketDetails TicketDetails { get; private set; }

        private Ticket() {}
        
        public Ticket(string owner, TicketDetails ticketDetails)
            : base()
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            TicketDetails = ticketDetails ?? throw new ArgumentNullException(nameof(ticketDetails));
        }

        public CompletedTicket Close()
        {
            return new CompletedTicket(new TicketDetails("foo", DateTime.Now, DateTime.Now));
        }

        public void SetDueDate (DateTime dueDate)
        {
            TicketDetails = new TicketDetails(TicketDetails.Description, TicketDetails.SubmitDate, dueDate);
        }

    }
}