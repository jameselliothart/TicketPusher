using System.Collections.Generic;
using System.Threading.Tasks;
using TicketPusher.DataTransfer;

namespace TicketPusher.Server.Templates
{
    public interface IEntityReadDataService<TDto>
    {
        Task<EnvelopeDto<List<TDto>>> GetEntityListAsync();
    }
}