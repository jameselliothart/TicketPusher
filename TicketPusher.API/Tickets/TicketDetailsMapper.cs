using AutoMapper;
using TicketPusher.Domain.Common;

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