using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;

namespace TicketPusher.API.CompletedTickets.Queries
{
    public class GetCompletedTicketListQuery : IRequest<Result<IEnumerable<CompletedTicketDto>>>
    {
        public GetCompletedTicketListQuery()
        {
        }
    }
}