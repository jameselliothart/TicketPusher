using System;
using System.Collections.Generic;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.SharedKernel;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.Domain.Tests.Utils
{
    public class TicketTestData
    {
        public static Ticket DefaultTicket()
        {
            var ticketDetails = new TicketDetails("desc", DateTime.Now, NoSetDate.Instance);
            return new Ticket("owner", ticketDetails);
        }

        public static IEnumerable<Ticket> TicketList()
        {
            DateTime[] dueDates = {NoSetDate.Instance, DateTime.Now.AddDays(7), DateTime.Now.AddMonths(1)};
            for (int i = 0; i < dueDates.Length; i++)
            {
                yield return new Ticket("Unassigned", new TicketDetails($"Ticket {i}", DateTime.Now, dueDates[i]));
            }
        }
    }
}