using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, Result<TicketDto>>
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
        public async Task<Result<TicketDto>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetTicketAsync(request.TicketId);
            var ticketToReturn = _mapper.Map<TicketDto>(ticket);
            return Result.SuccessIf<TicketDto>(ticketToReturn != null, ticketToReturn, $"Ticket not found for Id {request.TicketId}");
        }
    }
}