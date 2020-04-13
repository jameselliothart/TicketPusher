using System;
using System.Collections.Generic;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tests.Utils
{
    public class TicketTestData
    {
        public static Ticket DefaultTicket()
        {
            return new Ticket("owner", "desc", DateTime.Now, NoSetDate.Instance);
        }

        public static IEnumerable<Ticket> TicketList()
        {
            yield return new Ticket("Unassigned", "Ticket 1", DateTime.Now, NoSetDate.Instance);
            yield return new Ticket("Unassigned", "Ticket 2", DateTime.Now, DateTime.Now.AddDays(7));
            yield return new Ticket("Unassigned", "Ticket 3", DateTime.Now, DateTime.Now.AddMonths(1));
        }
    }
}