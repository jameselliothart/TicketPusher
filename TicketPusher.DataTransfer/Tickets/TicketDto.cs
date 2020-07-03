using System;

namespace TicketPusher.DataTransfer.Tickets
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public TicketDetailsDto TicketDetails { get; set; }
        public Guid ProjectId { get; set; }
    }
}