using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using TicketPusher.API.CompletedTickets;
using TicketPusher.API.Tests.Utils;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.CompletedTickets
{
    public class CompletedTicketsControllerShould : IClassFixture<WebApplicationFixture>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFixture _factory;

        public CompletedTicketsControllerShould(WebApplicationFixture factory)
        {
            _factory = factory;
            _client = _factory
                .WithWebHostBuilder(WebApplicationFixture.BuildWebHost(db => {}))
                .CreateClient();
        }

        [Fact]
        public async Task RetrieveCompletedTickets()
        {
            // Arrange
            // Call ToList() or else the Id of the underlying tickets mutates each time they are enumerated
            var completedTickets = TicketTestData.TicketList().Select(t => t.Close("Done.")).ToList();
            var client = _factory.WithWebHostBuilder(
                WebApplicationFixture.BuildWebHost(db =>
                {
                    db.CompletedTickets.AddRange(completedTickets);
                    db.SaveChanges();
                }))
                .CreateClient();

            // Act
            var httpResponse = await client.GetAsync($"/api/completedtickets");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var envelope = JsonConvert.DeserializeObject<Envelope<IEnumerable<CompletedTicketDto>>>(stringResponse);
            envelope.Result.Select(t => t.Id).Should().BeEquivalentTo(completedTickets.Select(t => t.Id));
        }
    }
}