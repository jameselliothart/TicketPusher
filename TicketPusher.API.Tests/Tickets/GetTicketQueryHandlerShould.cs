using System.Threading;
using System.Threading.Tasks;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using FluentAssertions;
using Xunit;
using TicketPusher.API.Tickets.Queries;
using TicketPusher.Domain.Tests.Utils;
using CSharpFunctionalExtensions;
using System;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tickets;
using FluentAssertions.Execution;

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketQueryHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public GetTicketQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public async Task GetTicketById()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            var expected = _mapper.Instance.Map<TicketDto>(ticket);
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }

            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                var repository = new TicketPusherRepository(context);
                var sutQueryHandler = new GetTicketQueryHandler(repository, _mapper.Instance);

                // Act
                var actual = await sutQueryHandler.Handle(new GetTicketQuery(expected.Id), new CancellationToken());

                // Assert
                actual.Value.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task ReturnNotFoundError_WhenTicketDoesNotExist()
        {
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                // Arrange
                var repository = new TicketPusherRepository(context);
                var sutQueryHandler = new GetTicketQueryHandler(repository, _mapper.Instance);
                var invalidId = Guid.NewGuid();

                // Act
                var actual = await sutQueryHandler.Handle(new GetTicketQuery(invalidId), new CancellationToken());

                // Assert
                using (new AssertionScope())
                {
                    actual.Error.Should().Be(Errors.General.NotFound());
                    actual.Error.Message.Should().Be($"'{nameof(Ticket)}' not found for Id '{invalidId}'");
                }
            }
        }
    }
}