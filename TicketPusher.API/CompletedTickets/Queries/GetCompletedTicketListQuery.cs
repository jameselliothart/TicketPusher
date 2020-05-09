using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Common;

namespace TicketPusher.API.CompletedTickets.Queries
{
    public class GetCompletedTicketListQuery : GetEntityListQuery<CompletedTicketDto>
    {
        public GetCompletedTicketListQuery() : base()
        {
        }
    }
}