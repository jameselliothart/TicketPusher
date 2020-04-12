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
    public class GetTicketQueryHandlerShould : IClassFixture<MapperFixture>
    {
        private readonly MapperFixture _mapper;

        public GetTicketQueryHandlerShould(MapperFixture mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public async Task GetTicketById()
        {
            // Arrange
            ITicketPusherRepository repo = new InMemoryRepository();
            var ticket = TicketTestData.DefaultTicket();
            repo.CreateTicket(ticket);
            var expected = _mapper.Instance.Map<TicketDto>(ticket);
            var sutQueryHandler = new GetTicketQueryHandler(repo, _mapper.Instance);

            // Act
            var actual = await sutQueryHandler.Handle(new GetTicketQuery(expected.Id), new CancellationToken());

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}