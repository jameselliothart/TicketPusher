using System;
using TicketPusher.API.Common;
using TicketPusher.DataTransfer.Tickets;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQuery : GetEntityQuery<TicketDto>
    {
        public GetTicketQuery(Guid ticketId) : base(ticketId)
        {
        }

    }
}