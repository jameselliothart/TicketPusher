using System.Net.Http;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class TicketWriteDataService : EntityWriteDataService<TicketDto, SubmitTicketDto>, ITicketWriteDataService
    {
        public TicketWriteDataService(HttpClient httpClient) : base(httpClient, "tickets")
        {
        }
    }
}