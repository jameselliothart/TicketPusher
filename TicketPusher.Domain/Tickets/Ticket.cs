using System;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.CompletedTickets;

namespace TicketPusher.Domain.Tickets
{
    public class Ticket : Entity
    {
        public string Owner { get; private set; }
        public string Description { get; private set; }
        public DateTime SubmitDate { get; private set; }
        public DateTime DueDate { get; private set; }
        
        public Ticket(string owner, string description, DateTime submitDate, DateTime dueDate)
            : base()
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Description = description ?? throw new ArgumentNullException(nameof(Description));
            SubmitDate = submitDate;
            DueDate = dueDate;
        }

        public CompletedTicket Close()
        {
            return new CompletedTicket();
        }

        public void SetDueDate (DateTime dueDate)
        {
            DueDate = dueDate;
        }

    }
}