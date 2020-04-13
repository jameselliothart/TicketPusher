using System;
using MediatR;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQuery : IRequest<TicketDto>
    {
        public GetTicketQuery(Guid ticketId)
        {
            TicketId = ticketId;
        }

        public Guid TicketId { get; }
    }
}