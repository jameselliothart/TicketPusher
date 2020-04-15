using System;
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
    }
}