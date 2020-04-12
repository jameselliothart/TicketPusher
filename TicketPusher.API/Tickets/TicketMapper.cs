using AutoMapper;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets
{
    public class TicketMapper : Profile
    {
        public TicketMapper()
        {
            CreateMap<Ticket, TicketDto>();
        }
    }
}