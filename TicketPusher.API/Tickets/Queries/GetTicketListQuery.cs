using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketListQuery : IRequest<Result<IEnumerable<TicketDto>>>
    {
        
    }
}