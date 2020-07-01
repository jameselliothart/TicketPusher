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

namespace TicketPusher.API.Tests.Tickets
{
    public class GetTicketQueryHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public GetTicketQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private Func<TicketPusherRepository, GetTicketQuery, Task<Result<TicketDto, Error>>> HandleQuery =
            async (repo, query) =>
            {
                var queryHandler = new GetTicketQueryHandler(repo, _mapper.Instance);
                return await queryHandler.Handle(query, new CancellationToken());
            };

        [Fact]
        public void GetTicketById()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            var query = new GetTicketQuery(ticket.Id);

            ActWithContext(async ctx => {
                ctx.Tickets.Add(ticket);
                await ctx.SaveChangesAsync();
            });

            ActWithRepository(async repo =>
            {
                // Act
                Result<TicketDto, Error> result = await HandleQuery(repo, query);

                // Assert
                result.Value.Id.Should().Be(ticket.Id);
            });
        }

        [Fact]
        public void ReturnNotFoundError_WhenTicketDoesNotExist()
        {
            ActWithRepository(async repo =>
            {
                // Arrange
                var query = new GetTicketQuery(Guid.NewGuid());

                // Act
                Result<TicketDto, Error> result = await HandleQuery(repo, query);

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            });
        }
    }
}