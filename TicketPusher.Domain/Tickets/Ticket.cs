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
            return Close(string.Empty);
        }

        public CompletedTicket Close(string resolution)
        {
            return new CompletedTicket(Owner, TicketDetails, new CompletedDetails(DateTime.Now, resolution));
        }

        public void SetDueDate (DateTime dueDate)
        {
            TicketDetails = new TicketDetails(TicketDetails.Description, TicketDetails.SubmitDate, dueDate);
        }

    }
}