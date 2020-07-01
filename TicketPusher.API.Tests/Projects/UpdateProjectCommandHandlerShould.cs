using System;
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
    public class UpdateProjectCommandHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public UpdateProjectCommandHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private void SetUpProjects(params Project[] projects)
        {
            ActWithContext(async ctx =>
            {
                ctx.Projects.AddRange(projects);
                await ctx.SaveChangesAsync();
            });
        }

        private async Task<Project> GetProjectFromDb(TicketPusherContext ctx, Guid id) =>
            await ctx.Projects.Include(p => p.ParentProject).SingleAsync(p => p.Id == id);

        private Func<TicketPusherRepository, UpdateProjectCommand, Task<Result<ProjectDto, Error>>> HandleCommand =
            async (repo, command) =>
            {
                var commandHandler = new UpdateProjectCommandHandler(repo, _mapper.Instance);
                return await commandHandler.Handle(command, new CancellationToken());
            };

        [Fact]
        public void SetTheParentProject_GivenAValidParent()
        {
            var project = new Project("SUT");
            var parentProject = new Project("Parent");
            var command = new UpdateProjectCommand(project.Id, name: null, parentProject.Id);

            SetUpProjects(project, parentProject);

            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            ActWithContext(async ctx =>
            {
                var actualProject = await GetProjectFromDb(ctx, project.Id);
                actualProject.ParentProject.Should().Be(parentProject);
            });
        }

        [Fact]
        public void SetTheParentProjectToNone_GivenNoParent()
        {
            var parentProject = new Project("Parent");
            var project = new Project("SUT", parentProject);
            var command = new UpdateProjectCommand(project.Id, name: null, Guid.Empty);

            SetUpProjects(project);

            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            ActWithContext(async ctx =>
            {
                var actualProject = await GetProjectFromDb(ctx, project.Id);
                actualProject.ParentProject.Should().Be(Project.None);
            });
        }

        [Fact]
        public void ReturnNotFound_GivenAnInvalidParent()
        {
            var project = new Project("SUT");
            var command = new UpdateProjectCommand(project.Id, name: null, Guid.NewGuid());

            SetUpProjects(project);

            ActWithRepository(async repo =>
            {
                Result<ProjectDto, Error> result = await HandleCommand(repo, command);

                result.Error.Should().Be(Errors.General.NotFound());
            });
        }

        [Fact]
        public void ReturnNotFound_GivenAnInvalidProject()
        {
            var command = new UpdateProjectCommand(Guid.NewGuid(), name: null, Guid.Empty);

            ActWithRepository(async repo =>
            {
                Result<ProjectDto, Error> result = await HandleCommand(repo, command);

                result.Error.Should().Be(Errors.General.NotFound());
            });
        }

        [Fact]
        public void NotFail_GivenTheExistingParent()
        {
            var project = new Project("SUT");
            var command = new UpdateProjectCommand(project.Id, name: null, Guid.Empty);

            SetUpProjects(project);

            ActWithRepository(async repo =>
            {
                await HandleCommand(repo, command);
            });

            ActWithContext(async ctx =>
            {
                var actualProject = await GetProjectFromDb(ctx, project.Id);
                actualProject.ParentProject.Should().Be(Project.None);
            });
        }
    }
}