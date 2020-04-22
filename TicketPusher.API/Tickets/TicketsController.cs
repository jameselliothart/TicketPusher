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
    [ApiController]
    [Route("api/tickets")]
    public sealed class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketList()
        {
            var query = new GetTicketListQuery();
            var result = await _mediator.Send(query);
            return Ok(Envelope.Ok(result.Value));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(Guid id)
        {
            var query = new GetTicketQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(Envelope.Ok(result.Value));

            if (result.Error == Errors.General.NotFound())
                return NotFound(Envelope.Error(result.Error.Message));

            return BadRequest(Envelope.Error(result.Error.Message));
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] SubmitTicketDto ticket)
        {
            var command = new SubmitTicketCommand(ticket.Owner, ticket.Description, ticket.DueDate, ticket.ProjectId);
            var result = await _mediator.Send(command);
            
            return CreatedAtAction(nameof(GetTicket),
                new { id = result.Id },
                result);
        }
    }
}