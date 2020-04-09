using System;

namespace TicketPusher.Domain.Tickets
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}