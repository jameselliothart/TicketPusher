using System;
using System.Threading.Tasks;
using TicketPusher.DataTransfer;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public interface ITicketWriteDataService : IEntityWriteDataService<TicketDto, SubmitTicketDto>
    {
        Task<EnvelopeDto<CompletedTicketDto>> CloseTicketAsync(Guid ticketId, CloseTicketDto dto);
    }
}