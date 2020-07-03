using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.API.Tickets.Queries;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.DataTransfer.Tickets;

namespace TicketPusher.API.Tickets
{
    [Route("api/tickets")]
    public sealed class TicketsController : ApiController
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketList()
        {
            var query = new GetTicketListQuery();
            Result<IEnumerable<TicketDto>> result = await _mediator.Send(query);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var query = new GetTicketQuery(id);
            Result<TicketDto, Error> result = await _mediator.Send(query);

            return ValueOrError(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] SubmitTicketDto ticket)
        {
            var command = new SubmitTicketCommand(ticket.Owner, ticket.Description, ticket.DueDate, ticket.ProjectId);
            Result<TicketDto, Error> result = await _mediator.Send(command);

            return ValueOrError(result);
        }

        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseTicket(Guid id, [FromBody] CloseTicketDto closeTicketDto)
        {
            var command = new CloseTicketCommand(id, closeTicketDto.Resolution);
            Result<CompletedTicketDto, Error> result = await _mediator.Send(command);

            return ValueOrError(result);
        }
    }
}