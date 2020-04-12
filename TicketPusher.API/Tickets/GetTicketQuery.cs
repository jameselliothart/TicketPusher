using System;
using MediatR;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets
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