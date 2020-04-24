using AutoMapper;
using TicketPusher.Domain.Common;

namespace TicketPusher.API.Common
{
    public class TicketDetailsMapper : Profile
    {
        public TicketDetailsMapper()
        {
            CreateMap<TicketDetails, TicketDetailsDto>();
        }
    }
}