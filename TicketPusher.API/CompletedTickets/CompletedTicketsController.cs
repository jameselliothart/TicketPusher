using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;
using TicketPusher.API.CompletedTickets.Queries;

namespace TicketPusher.API.CompletedTickets
{
    [Route("api/completedtickets")]
    public class CompletedTicketsController : ApiController
    {
        private readonly IMediator _mediator;

        public CompletedTicketsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetCompletedTicketList()
        {
            var query = new GetCompletedTicketListQuery();
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }
    }
}