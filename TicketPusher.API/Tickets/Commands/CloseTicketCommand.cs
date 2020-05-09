using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.CompletedTickets;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Tickets.Commands
{
    public class CloseTicketCommand : IRequest<Result<CompletedTicketDto, Error>>
    {
        public CloseTicketCommand(Guid ticketId, string resolution)
        {
            TicketId = ticketId;
            Resolution = resolution;
        }

        public Guid TicketId { get; }
        public string Resolution { get; }
    }
}