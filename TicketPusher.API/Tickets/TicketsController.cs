using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.API.Tickets.Queries;

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

        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseTicket(Guid id, [FromBody] CloseTicketDto closeTicketDto)
        {
            var command = new CloseTicketCommand(id, closeTicketDto.Resolution);
            var result = await _mediator.Send(command);

            return FromResultWithValue(result);
        }
    }
}