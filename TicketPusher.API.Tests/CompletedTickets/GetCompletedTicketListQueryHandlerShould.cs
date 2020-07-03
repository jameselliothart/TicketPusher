using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CSharpFunctionalExtensions;
using FluentAssertions;
using TicketPusher.API.CompletedTickets.Queries;
using TicketPusher.API.Tests.Utils;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.Domain.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.CompletedTickets
{
    public class GetCompletedTicketListQueryHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public GetCompletedTicketListQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        [Fact]
        public void ReturnCompletedTickets()
        {
            // Arrange
            var query = new GetCompletedTicketListQuery();
            var completedTickets = TicketTestData.TicketList().Select(t => t.Close("Done.")).ToList();

            ActWithContext(async ctx =>
            {
                ctx.CompletedTickets.AddRange(completedTickets);
                await ctx.SaveChangesAsync();
            });

            ActWithRepository(async repo =>
            {
                // Act
                var queryHandler = new GetCompletedTicketListQueryHandler(repo, _mapper.Instance);
                Result<IEnumerable<CompletedTicketDto>> result = await queryHandler.Handle(query, new CancellationToken());

                // Assert
                result.Value.Select(t => t.Id).Should().BeEquivalentTo(completedTickets.Select(t => t.Id));
            });
        }
    }
}