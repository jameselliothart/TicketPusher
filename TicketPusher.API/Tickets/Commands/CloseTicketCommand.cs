using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.CompletedTickets;

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