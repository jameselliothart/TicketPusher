using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets.Commands
{
    public class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommand, Result<CompletedTicketDto, Error>>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public CloseTicketCommandHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Result<CompletedTicketDto, Error>> Handle(CloseTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToClose = await _repository.GetTicketAsync(request.TicketId);
            var result = await Result
                .SuccessIf(ticketToClose != null, ticketToClose, Errors.General.NotFound(nameof(Ticket), request.TicketId))
                .Map(t => _repository.CloseTicket(t, request.Resolution))
                .Tap(async () => await _repository.SaveChangesAsync())
                .Map(ct => _mapper.Map<CompletedTicketDto>(ct))
                ;

            return result;
        }
    }
}