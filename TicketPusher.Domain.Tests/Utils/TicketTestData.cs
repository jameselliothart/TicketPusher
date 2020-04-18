using System;
using System.Collections.Generic;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.Projects;
using TicketPusher.Domain.SharedKernel;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.Domain.Tests.Utils
{
    public class TicketTestData
    {
        public static Ticket DefaultTicket()
        {
            var ticketDetails = new TicketDetails("desc", DateTime.Now, NoSetDate.Instance);
            var project = new Project("None");
            return new Ticket("owner", project, ticketDetails);
        }

        public static IEnumerable<Ticket> TicketList()
        {
            var project = new Project("None");
            DateTime[] dueDates = {NoSetDate.Instance, DateTime.Now.AddDays(7), DateTime.Now.AddMonths(1)};
            for (int i = 0; i < dueDates.Length; i++)
            {
                yield return new Ticket("Unassigned", project, new TicketDetails($"Ticket {i}", DateTime.Now, dueDates[i]));
            }
        }
    }
}