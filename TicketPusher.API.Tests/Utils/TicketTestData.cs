using System;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tests.Utils
{
    public class TicketTestData
    {
        public static Ticket DefaultTicket()
        {
            return new Ticket("owner", "desc", DateTime.Now, NoSetDate.Instance);
        }
    }
}