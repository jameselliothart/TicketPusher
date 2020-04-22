using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQuery : IRequest<Result<TicketDto>>
    {
        public GetTicketQuery(Guid ticketId)
        {
            TicketId = ticketId;
        }

        public Guid TicketId { get; }
    }
}