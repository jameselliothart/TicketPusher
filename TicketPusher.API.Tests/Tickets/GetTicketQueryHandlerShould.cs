using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.Domain.Tickets;
using FluentAssertions;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketQueryHandlerShould
    {
        [Fact]
        public async Task GetTicketById()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TicketMapper());
            });
            var mapper = config.CreateMapper();
            ITicketPusherRepository repo = new InMemoryRepository();
            var ticket = CreateTestTicket();
            repo.CreateTicket(ticket);
            var expected = mapper.Map<TicketDto>(ticket);
            var sutQueryHandler = new GetTicketQueryHandler(repo, mapper);
            CancellationToken token = new CancellationToken();

            // Act
            var actual = await sutQueryHandler.Handle(new GetTicketQuery(expected.Id), token);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        private Ticket CreateTestTicket()
        {
            var ticketId = Guid.NewGuid();
            return new Ticket(ticketId, "owner", "desc", DateTime.Now, NoSetDate.Instance);
        }
    }
}