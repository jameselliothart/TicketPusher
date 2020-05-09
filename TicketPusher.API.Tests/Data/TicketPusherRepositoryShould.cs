using System;
using System.Linq;
using System.Threading.Tasks;
using TicketPusher.API.Tests.Utils;
using TicketPusher.Domain.Tests.Utils;
using TicketPusher.Domain.Tickets;
using Xunit;

namespace TicketPusher.API.Tests.Data
{

    public class TicketPusherRepositoryShould : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _db;

        public TicketPusherRepositoryShould(DatabaseFixture db)
        {
            _db = db;
        }

        [Fact]
        public void CreateATicket()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            
            // Act
            _db.repository.CreateTicket(ticket);
            _db.repository.SaveChanges();

            // Assert
            var ticketFromRepo = _db.context.Tickets.FirstOrDefault(t => t.Id == ticket.Id);
            Assert.Equal(ticket, ticketFromRepo);
        }

        [Fact]
        public async Task RetrieveATicket()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            _db.context.Tickets.Add(ticket);
            _db.context.SaveChanges();

            // Act
            Ticket ticketFromRepo = await _db.repository.GetTicketAsync(ticket.Id);

            // Assert
            Assert.Equal(ticket, ticketFromRepo);
        }

    }

}