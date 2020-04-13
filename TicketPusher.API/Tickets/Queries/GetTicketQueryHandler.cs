using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TicketPusher.API.Data;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, TicketDto>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<TicketDto> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetTicketAsync(request.TicketId);
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}