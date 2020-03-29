using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPusher.Server.Data
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime DueDate { get; set; }

        public Ticket(string owner, string description, DateTime? dueDate)
        {
            Id = Guid.NewGuid();
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SubmitDate = DateTime.Now;
            DueDate = dueDate ?? DateTime.Now;
        }
    }
}
