using System;
using FluentAssertions;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.CompletedTickets;
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
            var sut = new CompletedTicket("Unassigned", ticketDetails, completedDetails);

            sut.Id.Should().NotBeEmpty();
        }
    }
}