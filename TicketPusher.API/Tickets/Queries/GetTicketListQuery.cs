using System.Collections.Generic;
using MediatR;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketListQuery : IRequest<IEnumerable<TicketDto>>
    {
        
    }
}