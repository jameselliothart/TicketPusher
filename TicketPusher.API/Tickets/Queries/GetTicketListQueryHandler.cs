using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TicketPusher.API.Data;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketListQueryHandler : IRequestHandler<GetTicketListQuery, IEnumerable<TicketDto>>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public GetTicketListQueryHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TicketDto>> Handle(GetTicketListQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _repository.GetAllTicketsAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }
    }
}