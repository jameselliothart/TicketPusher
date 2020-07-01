using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.API.Tests.Projects
{
    public class CreateProjectCommandHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public CreateProjectCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private Func<TicketPusherContext, string, Task<Project>> GetProjectFromDb = async (ctx, projectName) =>
            await ctx.Projects.Where(p => p.Name == projectName).Include(p => p.ParentProject).SingleOrDefaultAsync();

        private Func<TicketPusherRepository, CreateProjectCommand, Task<Result<ProjectDto, Error>>> HandleCommand =
            async (repo, command) =>
            {
                var commandHandler = new CreateProjectCommandHandler(repo, _mapper.Instance);
                return await commandHandler.Handle(command, new CancellationToken());
            };

        [Fact]
        public void CreateAProject_WithNoParent()
        {
            // Arrange
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, Guid.Empty);

            // Act
            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            // Assert
            ActWithContext(async ctx =>
            {
                var projectFromDb = await GetProjectFromDb(ctx, projectName);
                projectFromDb.ParentProject.Should().Be(Project.None);
            });

        }

        [Fact]
        public void CreateAProject_WithAParent()
        {
            // Arrange
            var parentProject = new Project("Parent");
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, parentProject.Id);

            // Act
            ActWithRepository(async repo =>
            {
                repo.CreateProject(parentProject);
                await repo.SaveChangesAsync();
                await HandleCommand(repo, command);
            });

            // Assert
            ActWithContext(async ctx =>
            {
                var projectFromDb = await GetProjectFromDb(ctx, projectName);
                projectFromDb.ParentProject.Should().Be(parentProject);
            });
        }

        [Fact]
        public void ReturnNotFound_WhenParentDoesNotExist()
        {
            // Arrange
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, Guid.NewGuid());

            ActWithRepository(async repo =>
            {
                // Act
                var result = await HandleCommand(repo, command);

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            });

            // Assert
            ActWithContext(async ctx =>
            {
                var project = await GetProjectFromDb(ctx, projectName);
                project.Should().BeNull("because a project with nonexistent parent should not be saved");
            });
        }

        [Fact]
        public void ReturnAProjectDto_WhenSuccessful()
        {
            // Arrange
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, Guid.Empty);

            ActWithRepository(async repo =>
            {
                // Act
                Result<ProjectDto, Error> result = await HandleCommand(repo, command);

                // Assert
                result.Value.Name.Should().Be(projectName);
            });
        }
    }
}