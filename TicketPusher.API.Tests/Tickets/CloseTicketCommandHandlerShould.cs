using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tests.Utils;
using TicketPusher.Domain.Tickets;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class CloseTicketCommandHandlerShould : QueryHandlerTestSetup, IClassFixture<MapperFixture>
    {
        public CloseTicketCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public async Task DeleteTheClosedTicket()
        {
            await AssertWithContext((context, ticket) =>
            {
                context.Tickets.Where(t => t.Id == ticket.Id).FirstOrDefault().Should().BeNull();
            });
        }

        [Fact]
        public async Task CreateACompletedTicket()
        {
            await AssertWithContext((context, ticket) =>
            {
                context.CompletedTickets.Where(t => t.Id == ticket.Id).FirstOrDefault().Id.Should().Be(ticket.Id);
            });
        }

        [Fact]
        public async Task ReturnNotFoundError_WhenTicketNotFound()
        {
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                // Arrange
                var repository = new TicketPusherRepository(context);
                var handler = new CloseTicketCommandHandler(repository, _mapper.Instance);
                var command = new CloseTicketCommand(Guid.NewGuid(), "Resolved");

                // Act
                var result = await handler.Handle(command, new CancellationToken());

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            }
        }

        private async Task AssertWithContext(Action<TicketPusherContext, Ticket> assertion)
        {

            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                // Arrange
                var repository = new TicketPusherRepository(context);
                var handler = new CloseTicketCommandHandler(repository, _mapper.Instance);
                var ticket = TicketTestData.DefaultTicket();
                context.Tickets.Add(ticket);
                await context.SaveChangesAsync();
                // verify test setup
                context.Tickets.Where(t => t.Id == ticket.Id).FirstOrDefault().Id.Should().Be(ticket.Id);

                var command = new CloseTicketCommand(ticket.Id, "Resolved");

                // Act
                var result = await handler.Handle(command, new CancellationToken());

                // Assert
                assertion(context, ticket);
            }
        }
    }
}