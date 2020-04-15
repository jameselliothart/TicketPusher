using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
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
            var submitTicketDto = new SubmitTicketDto()
                {
                    Owner = "Unassigned",
                    Description = $"{Guid.NewGuid()}"
                };
            var httpResponse = await _client.PostAsync("/api/tickets", submitTicketDto.JsonContent());

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
            var ticketDto = JsonConvert.DeserializeObject<TicketDto>(stringResponse);
            Assert.Equal(ticket.Id, ticketDto.Id);
        }
    }
}