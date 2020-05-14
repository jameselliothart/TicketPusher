using System;
using TicketPusher.Server.Tickets;

namespace TicketPusher.Server.CompletedTickets
{
    public class CompletedTicketDto
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public Guid ProjectId { get; set; }
        public TicketDetailsDto TicketDetails { get; set; }
        public CompletedDetailsDto CompletedDetails { get; set; }
    }
}