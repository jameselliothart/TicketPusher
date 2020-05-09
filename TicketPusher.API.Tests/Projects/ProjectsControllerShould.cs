using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicketPusher.API.Projects;
using TicketPusher.API.Projects.Commands;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Projects
{
    public class ProjectsControllerShould : IClassFixture<WebApplicationFixture>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFixture _factory;

        public ProjectsControllerShould(WebApplicationFixture factory)
        {
            _factory = factory;
            _client = _factory
                .WithWebHostBuilder(WebApplicationFixture.BuildWebHost(db => {}))
                .CreateClient();
        }

        [Fact]
        public async Task CreateAProject()
        {
            // Arrange
            var projectName = Guid.NewGuid().ToString();
            var createProjectDto = new CreateProjectDto()
            {
                Name = projectName
            };

            // Act
            var httpResponse = await _client.PostAsync("/api/projects", createProjectDto.JsonContent());

            // Assert
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var envelope = JsonConvert.DeserializeObject<Envelope<ProjectDto>>(stringResponse);
            Assert.Equal(createProjectDto.Name, envelope.Result.Name);
        }

        [Fact]
        public async Task RetrieveAProject()
        {
            // Arrange
            var project = TicketTestData.DefaultProject;
            var client = _factory.WithWebHostBuilder(
                WebApplicationFixture.BuildWebHost(db =>
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                }))
                .CreateClient();

            // Act
            var httpResponse = await client.GetAsync($"/api/projects/{project.Id}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var envelope = JsonConvert.DeserializeObject<Envelope<ProjectDto>>(stringResponse);
            Assert.Equal(project.Id, envelope.Result.Id);
        }
    }
}