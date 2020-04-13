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
    public class GetTicketQueryHandlerShould : IClassFixture<MapperFixture>
    {
        private readonly MapperFixture _mapper;
        private readonly ITicketPusherRepository _repository;

        public GetTicketQueryHandlerShould(MapperFixture mapper)
        {
            _mapper = mapper;
            _repository = new InMemoryRepository();
        }

        [Fact]
        public async Task GetTicketById()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            _repository.CreateTicket(ticket);

            var expected = _mapper.Instance.Map<TicketDto>(ticket);
            var sutQueryHandler = new GetTicketQueryHandler(_repository, _mapper.Instance);

            // Act
            var actual = await sutQueryHandler.Handle(new GetTicketQuery(expected.Id), new CancellationToken());

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}