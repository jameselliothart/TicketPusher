using AutoMapper;
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets
{
    public class TicketDetailsMapper : Profile
    {
        public TicketDetailsMapper()
        {
            CreateMap<TicketDetails, TicketDetailsDto>();
        }
    }
}