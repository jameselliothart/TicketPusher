using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Common;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;
using TicketPusher.Domain.CompletedTickets;

namespace TicketPusher.API.CompletedTickets.Queries
{
    public class GetCompletedTicketListQueryHandler : GetEntityListQueryHandler<GetCompletedTicketListQuery, CompletedTicketDto, CompletedTicket>
    {
        public GetCompletedTicketListQueryHandler(ITicketPusherRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override async Task<List<CompletedTicket>> GetEntitiesListAsync()
        {
            return await _repository.GetCompletedTicketsAsync();
        }
    }
}