using System.Threading;
using System.Threading.Tasks;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using FluentAssertions;
using Xunit;
using TicketPusher.API.Tickets.Queries;

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketQueryHandlerShould : QueryHandlerTestSetup, IClassFixture<MapperFixture>
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
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}