using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketPusher.API.Common;
using TicketPusher.API.CompletedTickets.Queries;
using TicketPusher.DataTransfer.CompletedTickets;

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
            Result<IEnumerable<CompletedTicketDto>> result = await _mediator.Send(query);
            return Ok(result.Value);
        }
    }
}