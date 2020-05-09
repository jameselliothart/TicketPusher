using System.Threading.Tasks;
using TicketPusher.Server.Shared;

namespace TicketPusher.Server.Templates
{
    public interface IEntityWriteDataService<TDto, TCreateDto>
    {
        Task<EnvelopeDto<TDto>> CreateEntityAsync(TCreateDto project);
    }
}