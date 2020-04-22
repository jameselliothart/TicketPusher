using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.API.Tickets.Queries;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tickets;

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
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var query = new GetTicketQuery(id);
            var result = await _mediator.Send(query);

            return FromResultWithValue(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] SubmitTicketDto ticket)
        {
            var command = new SubmitTicketCommand(ticket.Owner, ticket.Description, ticket.DueDate, ticket.ProjectId);
            var result = await _mediator.Send(command);

            return FromResultWithValue(result);
        }
    }
}