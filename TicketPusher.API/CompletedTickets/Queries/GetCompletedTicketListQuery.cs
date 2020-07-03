using TicketPusher.API.Common;
using TicketPusher.DataTransfer.CompletedTickets;

namespace TicketPusher.API.CompletedTickets.Queries
{
    public class GetCompletedTicketListQuery : GetEntityListQuery<CompletedTicketDto>
    {
        public GetCompletedTicketListQuery() : base()
        {
        }
    }
}