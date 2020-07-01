using System;
using FluentAssertions;
using TicketPusher.API.Projects.Commands;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.API.Tests.Projects
{
    public class UpdateProjectCommandShould
    {
        [Fact]
        public void InitializeWithNoneProjectId_GivenEmptyGuid()
        {
            var command = new UpdateProjectCommand(Guid.NewGuid(), null, Guid.Empty);

            command.ParentProjectId.Should().Be(Project.None.Id, "an empty parent id is assumed to be the None project");
        }
    }
}