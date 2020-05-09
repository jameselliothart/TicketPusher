using TicketPusher.API.Tickets;
using TicketPusher.API.Tickets.Commands;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public interface ITicketReadDataService : IEntityReadDataService<TicketDto>
    {
    }
}