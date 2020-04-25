using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;

namespace TicketPusher.API.CompletedTickets.Queries
{
    public class GetCompletedTicketListQueryHandler : IRequestHandler<GetCompletedTicketListQuery, Result<IEnumerable<CompletedTicketDto>>>
    {
        private readonly ITicketPusherRepository _repository;
        private readonly IMapper _mapper;

        public GetCompletedTicketListQueryHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<CompletedTicketDto>>> Handle(GetCompletedTicketListQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _repository.GetCompletedTicketsAsync();
            return Result.Success(_mapper.Map<IEnumerable<CompletedTicketDto>>(tickets));
        }
    }
}