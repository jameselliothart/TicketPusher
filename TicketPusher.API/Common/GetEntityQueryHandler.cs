using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using TicketPusher.API.Data;
using TicketPusher.API.Utils;

namespace TicketPusher.API.Common
{
    public abstract class GetEntityQueryHandler<TQuery, TDto, TEntity> : IRequestHandler<TQuery, Result<TDto, Error>>
        where TQuery : GetEntityQuery<TDto>
    {
        protected readonly ITicketPusherRepository _repository;
        protected readonly IMapper _mapper;

        public GetEntityQueryHandler(ITicketPusherRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TDto, Error>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            TEntity entity = await GetEntityAsync(request.Id);
            var entityToReturn = _mapper.Map<TDto>(entity);
            return Result.SuccessIf<TDto, Error>(entityToReturn != null, entityToReturn,
                Errors.General.NotFound(typeof(TEntity).Name, request.Id));
        }

        internal abstract Task<TEntity> GetEntityAsync(Guid id);

    }
}