using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public class TicketsControllerShould : IClassFixture<WebApplicationFixture>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFixture _factory;

        public TicketsControllerShould(WebApplicationFixture factory)
        {
            _factory = factory;
            _client = _factory
                .WithWebHostBuilder(WebApplicationFixture.BuildWebHost(db => {}))
                .CreateClient();
        }

        [Fact]
        public async Task CreateATicket()
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
            var submitTicketDto = new SubmitTicketDto()
                {
                    Owner = "Unassigned",
                    Description = $"{Guid.NewGuid()}",
                    ProjectId = project.Id
                };

            // Act
            var httpResponse = await client.PostAsync("/api/tickets", submitTicketDto.JsonContent());

            // Assert
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var ticket = JsonConvert.DeserializeObject<TicketDto>(stringResponse);
            Assert.Equal(submitTicketDto.Description, ticket.Description);
        }

        [Fact]
        public async Task RetrieveATicket()
        {
            // Arrange
            var ticket = TicketTestData.DefaultTicket();
            var client = _factory.WithWebHostBuilder(
                WebApplicationFixture.BuildWebHost(db =>
                {
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }))
                .CreateClient();

            // Act
            var httpResponse = await client.GetAsync($"/api/tickets/{ticket.Id}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var envelope = JsonConvert.DeserializeObject<Envelope<TicketDto>>(stringResponse);
            Assert.Equal(ticket.Id, envelope.Result.Id);
        }

        [Fact]
        public async Task ReturnNotFound_WhenTicketNotFound()
        {
            // Arrange
            var invalidId = Guid.NewGuid();

            // Act
            var httpResponse = await _client.GetAsync($"/api/tickets/{invalidId}");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task BeEmpty_WhenInitialized()
        {
            var httpResponse = await _client.GetAsync("/api/tickets");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var tickets = JsonConvert.DeserializeObject<IEnumerable<TicketDto>>(stringResponse);
            tickets.Should().BeEmpty();

        }
    }
}