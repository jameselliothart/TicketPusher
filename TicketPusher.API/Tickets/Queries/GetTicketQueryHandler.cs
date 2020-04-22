using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets.Queries
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, Result<TicketDto, Error>>
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
        public async Task<Result<TicketDto, Error>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetTicketAsync(request.TicketId);
            var ticketToReturn = _mapper.Map<TicketDto>(ticket);
            return Result.SuccessIf<TicketDto, Error>(ticketToReturn != null, ticketToReturn, Errors.General.NotFound(nameof(Ticket), request.TicketId));
        }
    }
}