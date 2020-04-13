using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Queries;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketListQueryHandlerShould : QueryHandlerTest, IClassFixture<MapperFixture>
    {
        public GetTicketListQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public async Task GetAllTickets_WhenPassedNoParameters()
        {
            // Arrange
            var seed = TicketTestData.TicketList().ToList();
            foreach (var ticket in seed)
            {
                _repository.CreateTicket(ticket);
            }
            await _repository.SaveChangesAsync();

            var expected = _mapper.Instance.Map<IEnumerable<TicketDto>>(seed);
            var sutQueryHandler = new GetTicketListQueryHandler(_repository, _mapper.Instance);

            // Act
            var actual = await sutQueryHandler.Handle(new GetTicketListQuery(), new CancellationToken());

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}