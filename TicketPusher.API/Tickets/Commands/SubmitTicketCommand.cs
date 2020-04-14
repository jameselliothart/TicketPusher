using System;
using MediatR;

namespace TicketPusher.API.Tickets.Commands
{
    public class SubmitTicketCommand : IRequest<TicketDto>
    {
        public SubmitTicketCommand(string owner, string description, DateTime dueDate)
        {
            Owner = owner;
            Description = description;
            DueDate = dueDate;
        }

        public string Owner { get; }
        public string Description { get; }
        public DateTime DueDate { get; }
    }
}