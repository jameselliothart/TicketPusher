using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.Domain.Tests.Utils;
using TicketPusher.Domain.Tickets;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class CloseTicketCommandHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public CloseTicketCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private CloseTicketCommand NewCloseTicketCommand(Ticket ticket) =>
            new CloseTicketCommand(ticket.Id, Guid.NewGuid().ToString());

        private Func<TicketPusherRepository, CloseTicketCommand, Task<Result<CompletedTicketDto, Error>>> HandleCommand =
            async (repo, command) =>
            {
                var commandHandler = new CloseTicketCommandHandler(repo, _mapper.Instance);
                return await commandHandler.Handle(command, new CancellationToken());
            };

        private void SaveToDb(Ticket ticket)
        {
            ActWithContext(async ctx =>
            {
                ctx.Tickets.Add(ticket);
                await ctx.SaveChangesAsync();
                // verify test setup
                var ticketFromDb = await ctx.Tickets.Where(t => t.Id == ticket.Id).SingleOrDefaultAsync();
                ticketFromDb.Should().NotBeNull();
            });
        }

        [Fact]
        public void DeleteTheClosedTicket()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            var command = NewCloseTicketCommand(ticket);
            SaveToDb(ticket);

            // Act
            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            // Assert
            ActWithContext(async ctx =>
            {
                var ticketFromDb = await ctx.Tickets.Where(t => t.Id == ticket.Id).SingleOrDefaultAsync();
                ticketFromDb.Should().BeNull();
            });
        }

        [Fact]
        public void CreateACompletedTicket()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            var command = NewCloseTicketCommand(ticket);
            SaveToDb(ticket);

            // Act
            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            // Assert
            ActWithContext(async ctx =>
            {
                var completedTicketFromDb = await ctx.CompletedTickets.Where(t => t.Id == ticket.Id).SingleOrDefaultAsync();
                completedTicketFromDb.Should().NotBeNull();
            });
        }

        [Fact]
        public void ReturnACompletedTicket()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            var command = NewCloseTicketCommand(ticket);
            SaveToDb(ticket);

            // Act
            ActWithRepository(async repo =>
            {
                Result<CompletedTicketDto, Error> result = await HandleCommand(repo, command);

                // Assert
                result.Value.CompletedDetails.Resolution.Should().Be(command.Resolution);
            });
        }

        [Fact]
        public void ReturnNotFoundError_WhenTicketNotFound()
        {
            ActWithRepository(async repo =>
            {
                // Arrange
                var command = new CloseTicketCommand(Guid.NewGuid(), string.Empty);

                // Act
                Result<CompletedTicketDto, Error> result = await HandleCommand(repo, command);

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            });
        }
    }
}