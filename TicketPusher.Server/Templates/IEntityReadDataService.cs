using System.Collections.Generic;
using System.Threading.Tasks;
using TicketPusher.Server.Shared;

namespace TicketPusher.Server.Templates
{
    public interface IEntityReadDataService<TDto>
    {
        Task<EnvelopeDto<List<TDto>>> GetEntityListAsync();
    }
}