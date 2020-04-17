using System;
using FluentAssertions;
using FluentAssertions.Execution;
using TicketPusher.Domain.CompletedTickets;
using TicketPusher.Domain.SharedKernel;
using TicketPusher.Domain.Tests.Utils;
using TicketPusher.Domain.Tickets;
using Xunit;

namespace TicketPusher.Domain.Tests
{
    public class TicketShould
    {
        [Fact]
        public void ReturnCompletedItem_WhenClosed()
        {
            // Arrange
            var sutTicket = TicketTestData.DefaultTicket();

            // Act
            var actual = sutTicket.Close();

            // Assert
            Assert.IsType<CompletedTicket>(actual);
        }

        [Fact(Skip = "TODO after refactoring existing logic")]
        public void RetainInformationInCompletedTicket_WhenClosed()
        {
            var sutTicket = TicketTestData.DefaultTicket();

            var actual = sutTicket.Close();

            // using (new AssertionScope())
            // {
            //     actual.Description.Should().Be(sutTicket.Description);
            // }
        }
    }
}