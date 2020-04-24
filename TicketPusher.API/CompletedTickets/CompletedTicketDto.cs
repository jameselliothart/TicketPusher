using System;
using TicketPusher.API.Common;

namespace TicketPusher.API.CompletedTickets
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