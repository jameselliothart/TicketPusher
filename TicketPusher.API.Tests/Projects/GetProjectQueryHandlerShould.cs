using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using TicketPusher.API.Data;
using TicketPusher.API.Projects.Queries;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;
using Xunit;

namespace TicketPusher.API.Tests.Projects
{
    public class GetProjectQueryHandlerShould : RequestHandlerShouldSetup, IClassFixture<MapperFixture>
    {
        public GetProjectQueryHandlerShould(MapperFixture mapper) : base(mapper)
        {
        }

        private Func<TicketPusherRepository, GetProjectQuery, Task<Result<ProjectDto, Error>>> HandleQuery =
            async (repo, query) =>
            {
                var queryHandler = new GetProjectQueryHandler(repo, _mapper.Instance);
                return await queryHandler.Handle(query, new CancellationToken());
            };

        [Fact]
        public void RetrieveAProject_WhenItExists()
        {
            var projectId = Project.None.Id;
            var query = new GetProjectQuery(projectId);

            ActWithRepository(async repo =>
            {
                Result<ProjectDto, Error> result = await HandleQuery(repo, query);

                // Assert
                result.Value.Id.Should().Be(projectId);
            });
        }

        [Fact]
        public void ReturnNotFound_WhenPassedAnInvalidId()
        {
            var query = new GetProjectQuery(Guid.NewGuid());

            ActWithRepository(async repo =>
            {
                Result<ProjectDto, Error> result = await HandleQuery(repo, query);

                // Assert
                result.Error.Should().Be(Errors.General.NotFound());
            });
        }
    }
}