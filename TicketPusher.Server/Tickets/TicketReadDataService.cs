using System.Net.Http;
using TicketPusher.API.Tickets;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class TicketReadDataService : EntityReadDataService<TicketDto>, ITicketReadDataService
    {
        public TicketReadDataService(HttpClient httpClient) : base(httpClient, "tickets")
        {
        }

    }
}