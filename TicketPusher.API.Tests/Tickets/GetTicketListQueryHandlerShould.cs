using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Queries;
using TicketPusher.Domain.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketListQueryHandlerShould : QueryHandlerTestSetup, IClassFixture<MapperFixture>
    {
        public GetTicketListQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public async Task GetAllTickets_WhenPassedNoParameters()
        {
            // Arrange
            var seed = TicketTestData.TicketList().ToList();
            var expected = _mapper.Instance.Map<IEnumerable<TicketDto>>(seed);
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                context.Tickets.AddRange(seed);
                context.SaveChanges();
            }

            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                var repository = new TicketPusherRepository(context);
                var sutQueryHandler = new GetTicketListQueryHandler(repository, _mapper.Instance);

                // Act
                var actual = await sutQueryHandler.Handle(new GetTicketListQuery(), new CancellationToken());

                // Assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}