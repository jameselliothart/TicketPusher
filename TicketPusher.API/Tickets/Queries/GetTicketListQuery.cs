using TicketPusher.API.Common;
using TicketPusher.DataTransfer.Tickets;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketListQuery : GetEntityListQuery<TicketDto>
    {
        public GetTicketListQuery() : base()
        {
        }
    }
}