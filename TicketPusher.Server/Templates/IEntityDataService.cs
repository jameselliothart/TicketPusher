using System.Collections.Generic;
using System.Threading.Tasks;
using TicketPusher.Server.Shared;

namespace TicketPusher.Server.Templates
{
    public interface IEntityDataService<TDto, TCreateDto>
    {
        Task<EnvelopeDto<TDto>> CreateEntityAsync(TCreateDto project);
        Task<EnvelopeDto<List<TDto>>> GetEntityListAsync();
    }
}