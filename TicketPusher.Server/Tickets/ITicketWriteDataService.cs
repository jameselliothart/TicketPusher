using System;
using System.Threading.Tasks;
using TicketPusher.Server.CompletedTickets;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public interface ITicketWriteDataService : IEntityWriteDataService<TicketDto, SubmitTicketDto>
    {
        Task<EnvelopeDto<CompletedTicketDto>> CloseTicketAsync(Guid ticketId, CloseTicketDto dto);
    }
}