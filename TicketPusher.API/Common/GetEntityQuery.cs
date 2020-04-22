using System;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Common
{
    public abstract class GetEntityQuery<TDto> : IRequest<Result<TDto, Error>>
    {
        public GetEntityQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}