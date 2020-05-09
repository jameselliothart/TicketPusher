using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;
using TicketPusher.Domain.Common;
using TicketPusher.Domain.Projects;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets.Commands
{
    public class SubmitTicketCommandHandler : IRequestHandler<SubmitTicketCommand, Result<TicketDto, Error>>
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
        public async Task<Result<TicketDto, Error>> Handle(SubmitTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketDetails = new TicketDetails(request.Description, DateTime.Now, request.DueDate);
            var project = await _repository.GetProjectAsync(request.ProjectId);

            var result = await Result.SuccessIf(project != null, project, Errors.General.NotFound(nameof(Project), request.ProjectId))
                .Map(async p => await SubmitTicket(request.Owner, p, ticketDetails))
                ;

            return result;
        }

        private async Task<TicketDto> SubmitTicket(string owner, Project project, TicketDetails ticketDetails)
        {
            var ticket = new Ticket(owner, project, ticketDetails);
            _repository.CreateTicket(ticket);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}