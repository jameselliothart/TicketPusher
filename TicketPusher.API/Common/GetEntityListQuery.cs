using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;

namespace TicketPusher.API.Common
{
    public abstract class GetEntityListQuery<TDto> : IRequest<Result<IEnumerable<TDto>>>
    {
        public GetEntityListQuery()
        {
        }
    }
}