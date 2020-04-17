using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class SubmitTicketCommandHandlerShould : QueryHandlerTestSetup, IClassFixture<MapperFixture>
    {
        public SubmitTicketCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public async Task CreateATicket()
        {
            // Arrange
            var command = DefaultSubmitTicketCommand();

            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                var repository = new TicketPusherRepository(context);
                var handler = new SubmitTicketCommandHandler(repository, _mapper.Instance);

                // Act
                await handler.Handle(command, new CancellationToken());
            }

            // Assert
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                var ticketFromDb = context.Tickets.Where(t => t.TicketDetails.Description == command.Description).Single();
                ticketFromDb.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task ReturnATicketDto()
        {
            // Arrange
            var command = DefaultSubmitTicketCommand();

            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                var repository = new TicketPusherRepository(context);
                var handler = new SubmitTicketCommandHandler(repository, _mapper.Instance);

                // Act
                var result = await handler.Handle(command, new CancellationToken());

                // Assert
                result.Should().BeOfType(typeof(TicketDto)).And
                    .BeEquivalentTo(new { Description = command.Description }, opt => opt.ExcludingMissingMembers());
            }
        }

        private SubmitTicketCommand DefaultSubmitTicketCommand()
        {
            return new SubmitTicketCommand("Unassigned", $"{Guid.NewGuid().ToString()}", DateTime.Now.AddDays(7));
        }
    }
}