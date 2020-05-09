using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TicketPusher.API.Common;
using TicketPusher.API.Data;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketListQueryHandler : GetEntityListQueryHandler<GetTicketListQuery, TicketDto, Ticket>
    {
        public GetTicketListQueryHandler(ITicketPusherRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override async Task<List<Ticket>> GetEntitiesListAsync()
        {
            return await _repository.GetAllTicketsAsync();
        }

    }
}