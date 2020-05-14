using System;

namespace TicketPusher.Server.Tickets
{
    public class TicketDetailsDto
    {
        public string Description { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}