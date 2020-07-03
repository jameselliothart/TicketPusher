using System.Threading.Tasks;
using TicketPusher.DataTransfer;

namespace TicketPusher.Server.Templates
{
    public interface IEntityWriteDataService<TDto, TCreateDto>
    {
        Task<EnvelopeDto<TDto>> CreateEntityAsync(TCreateDto project);
    }
}