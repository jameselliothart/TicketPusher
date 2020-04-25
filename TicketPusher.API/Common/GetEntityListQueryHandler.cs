using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Common
{
    public abstract class GetEntityListQueryHandler<TQuery, TDto, TEntity> : IRequestHandler<TQuery, Result<IEnumerable<TDto>>>
        where TQuery : GetEntityListQuery<TDto>
    {
        protected readonly ITicketPusherRepository _repository;
        protected readonly IMapper _mapper;

        public GetEntityListQueryHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<IEnumerable<TDto>>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            List<TEntity> tickets = await GetEntitiesListAsync();
            return Result.Success(_mapper.Map<IEnumerable<TDto>>(tickets));
        }

        protected abstract Task<List<TEntity>> GetEntitiesListAsync();
    }
}