using System;
using System.Threading.Tasks;
using AutoMapper;
using TicketPusher.API.Common;
using TicketPusher.API.Data;
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQueryHandler : GetEntityQueryHandler<GetTicketQuery, TicketDto, Ticket>
    {
        public GetTicketQueryHandler(ITicketPusherRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        internal override async Task<Ticket> GetEntityAsync(Guid id) =>
            await _repository.GetTicketAsync(id);

    }
}