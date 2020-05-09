using System;
using System.Threading.Tasks;
using TicketPusher.API.CompletedTickets;
using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.Server.Shared;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public interface ITicketWriteDataService : IEntityWriteDataService<TicketDto, SubmitTicketDto>
    {
        Task<EnvelopeDto<CompletedTicketDto>> CloseTicketAsync(Guid ticketId, CloseTicketDto dto);
    }
}