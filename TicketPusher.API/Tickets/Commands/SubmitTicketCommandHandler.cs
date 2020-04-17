using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets.Commands
{
    public class SubmitTicketCommandHandler : IRequestHandler<SubmitTicketCommand, TicketDto>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public SubmitTicketCommandHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<TicketDto> Handle(SubmitTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket(request.Owner, new TicketDetails(request.Description, DateTime.Now, request.DueDate));
            _repository.CreateTicket(ticket);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}