using TicketPusher.API.Common;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketListQuery : GetEntityListQuery<TicketDto>
    {
        public GetTicketListQuery() : base()
        {
        }
    }
}