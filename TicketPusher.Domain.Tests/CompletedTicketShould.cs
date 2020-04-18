using System;
using FluentAssertions;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.CompletedTickets;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.Domain.Tests
{
    public class CompletedTicketShould
    {
        [Fact]
        public void BeAssignedAnId()
        {
            var ticketDetails = new TicketDetails("Ticket", DateTime.Now, DateTime.Now.AddDays(7));
            var completedDetails = new CompletedDetails(DateTime.Now, "Done.");
            var project = new Project("None");
            var sut = new CompletedTicket("Unassigned", project, ticketDetails, completedDetails);

            sut.Id.Should().NotBeEmpty();
        }
    }
}