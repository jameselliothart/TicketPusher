using System;
using TicketPusher.Domain.Common;

namespace TicketPusher.Domain.Tickets
{
    public class Ticket : Entity
    {
        public string Owner { get; private set; }
        public string Description { get; private set; }
        public DateTime SubmitDate { get; private set; }
        public DateTime DueDate { get; private set; }
        
        public Ticket(Guid id, string owner, string description, DateTime submitDate, DateTime dueDate)
            : base(id)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Description = description ?? throw new ArgumentNullException(nameof(Description));
            SubmitDate = submitDate;
            DueDate = dueDate;
        }

        public void SetDueDate (DateTime dueDate)
        {
            DueDate = dueDate;
        }

    }
}