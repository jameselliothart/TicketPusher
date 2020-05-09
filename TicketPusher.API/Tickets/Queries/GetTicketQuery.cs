using System;
using TicketPusher.API.Common;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQuery : GetEntityQuery<TicketDto>
    {
        public GetTicketQuery(Guid ticketId) : base(ticketId)
        {
        }

    }
}