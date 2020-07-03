using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.Tickets;

namespace TicketPusher.API.Tickets.Commands
{
    public class SubmitTicketCommand : IRequest<Result<TicketDto, Error>>
    {
        public SubmitTicketCommand(string owner, string description, DateTime dueDate, Guid projectId)
        {
            Owner = owner;
            Description = description;
            DueDate = dueDate;
            ProjectId = projectId;
        }

        public string Owner { get; }
        public string Description { get; }
        public DateTime DueDate { get; }
        public Guid ProjectId { get; }
    }
}