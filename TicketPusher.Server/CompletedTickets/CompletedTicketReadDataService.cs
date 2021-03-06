using System.Net.Http;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.CompletedTickets
{
    public class CompletedTicketReadDataService : EntityReadDataService<CompletedTicketDto>, ICompletedTicketReadDataService
    {
        public CompletedTicketReadDataService(HttpClient httpClient) : base(httpClient, "completedtickets")
        {
        }
    }
}