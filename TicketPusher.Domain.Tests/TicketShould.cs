using System;
using TicketPusher.Domain.CompletedTickets;
using TicketPusher.Domain.SharedKernel;
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
            var sutTicket = new Ticket("Unassigned", "A unique description", DateTime.Now, NoSetDate.Instance);

            // Act
            var actual = sutTicket.Close();

            // Assert
            Assert.IsType<CompletedTicket>(actual);
        }
    }
}