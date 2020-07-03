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
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class SubmitTicketCommandHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public SubmitTicketCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private SubmitTicketCommand DefaultSubmitTicketCommand() =>
            new SubmitTicketCommand("Unassigned", Guid.NewGuid().ToString(), DateTime.Now.AddDays(7), Project.None.Id);

        private Func<TicketPusherRepository, SubmitTicketCommand, Task<Result<TicketDto, Error>>> HandleCommand =
            async (repo, command) =>
        {
            var commandHandler = new SubmitTicketCommandHandler(repo, _mapper.Instance);
            return await commandHandler.Handle(command, new CancellationToken());
        };

        [Fact]
        public void CreateATicket()
        {
            var command = DefaultSubmitTicketCommand();

            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            // Assert
            ActWithContext(async ctx =>
            {
                var ticketFromDb = await ctx.Tickets.Where(t => t.TicketDetails.Description == command.Description).SingleAsync();
                ticketFromDb.Should().NotBeNull();
            });
        }

        [Fact]
        public void ReturnATicketDto()
        {
            var command = DefaultSubmitTicketCommand();

            ActWithRepository(async repo =>
            {
                Result<TicketDto, Error> result = await HandleCommand(repo, command);

                // Assert
                result.Value.TicketDetails.Description.Should().Be(command.Description);
            });

        }

        [Fact]
        public void ReturnNotFound_GivenInvalidProject()
        {
            var command = new SubmitTicketCommand("Unassigned", "Test", DateTime.Now, Guid.NewGuid());

            ActWithRepository(async repo =>
            {
                Result<TicketDto, Error> result = await HandleCommand(repo, command);

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            });
        }
    }
}