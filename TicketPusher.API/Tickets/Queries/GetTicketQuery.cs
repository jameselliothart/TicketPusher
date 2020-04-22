using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQuery : IRequest<Result<TicketDto, Error>>
    {
        public GetTicketQuery(Guid ticketId)
        {
            TicketId = ticketId;
        }

        public Guid TicketId { get; }
    }
}