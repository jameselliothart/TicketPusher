using System;

namespace TicketPusher.API.CompletedTickets
{
    public class CompletedDetailsDto
    {
        public DateTime CompletionDate { get; set; }
        public string Resolution { get; set; }
    }
}