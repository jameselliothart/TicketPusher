using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.API.Tests.Projects
{
    public class CreateProjectCommandHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public CreateProjectCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private Func<TicketPusherContext, string, Task<Project>> GetProjectFromDb = (ctx, projectName) =>
            ctx.Projects.Where(p => p.Name == projectName).Include(p => p.ParentProject).SingleAsync();

        private Func<TicketPusherRepository, IMapper, CreateProjectCommand, Task<Result<ProjectDto, Error>>> HandleCommand =
            async (repo, mapper, command) =>
            {
                var handler = new CreateProjectCommandHandler(repo, mapper);
                return await handler.Handle(command, new CancellationToken());
            };

        [Fact]
        public void CreateAProject_WithNoParent()
        {
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, Guid.Empty);

            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, _mapper.Instance, command);
            });

            AssertWithContext(async ctx =>
            {
                var projectFromDb = await GetProjectFromDb(ctx, projectName);
                projectFromDb.ParentProject.Should().Be(Project.None);
            });

        }

        [Fact]
        public void CreateAProject_WithAParent()
        {
            var parentProject = new Project("Parent");
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, parentProject.Id);

            ActWithRepository(async repo =>
            {
                repo.CreateProject(parentProject);
                await repo.SaveChangesAsync();
                await HandleCommand(repo, _mapper.Instance, command);
            });

            AssertWithContext(async ctx =>
            {
                var projectFromDb = await GetProjectFromDb(ctx, projectName);
                projectFromDb.ParentProject.Should().Be(parentProject);
            });
        }

        [Fact]
        public void ReturnNotFound_WhenParentDoesNotExist()
        {
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, Guid.NewGuid());

            ActWithRepository(async repo =>
            {
                var result = await HandleCommand(repo, _mapper.Instance, command);

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            });

            AssertWithContext(ctx =>
            {
                Func<Task<Project>> act = async () => await GetProjectFromDb(ctx, projectName);
                act.Should().Throw<InvalidOperationException>("because a project with nonexistent parent should not be saved");
            });
        }

        [Fact]
        public void ReturnAProjectDto_WhenSuccessful()
        {
            var projectName = Guid.NewGuid().ToString();
            var command = new CreateProjectCommand(projectName, Guid.Empty);

            ActWithRepository(async repo =>
            {
                Result<ProjectDto, Error> result = await HandleCommand(repo, _mapper.Instance, command);

                // Assert
                result.Value.Name.Should().Be(projectName);
            });
        }
    }
}