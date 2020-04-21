using System;

namespace TicketPusher.API.Tickets
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
    }
}