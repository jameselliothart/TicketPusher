using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Queries;
using TicketPusher.Domain.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketListQueryHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public GetTicketListQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public void GetAllTickets_WhenPassedNoParameters()
        {
            // Arrange
            var query = new GetTicketListQuery();
            var seed = TicketTestData.TicketList().ToList();

            ActWithContext(async ctx =>
            {
                ctx.Tickets.AddRange(seed);
                await ctx.SaveChangesAsync();
            });

            ActWithRepository(async repo =>
            {
                // Act
                var queryHandler = new GetTicketListQueryHandler(repo, _mapper.Instance);
                Result<IEnumerable<TicketDto>> actual = await queryHandler.Handle(query, new CancellationToken());

                // Assert
                actual.Value.Select(t => t.Id).Should().BeEquivalentTo(seed.Select(t => t.Id));
            });
        }
    }
}