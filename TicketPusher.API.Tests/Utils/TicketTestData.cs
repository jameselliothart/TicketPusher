using System;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tests.Utils
{
    public class TicketTestData
    {
        public static Ticket DefaultTicket()
        {
            var ticketId = Guid.NewGuid();
            return new Ticket(ticketId, "owner", "desc", DateTime.Now, NoSetDate.Instance);
        }
    }
}