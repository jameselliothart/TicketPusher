using AutoMapper;
using TicketPusher.Domain.CompletedTickets;

namespace TicketPusher.API.CompletedTickets
{
    public class CompletedDetailsMapper : Profile
    {
        public CompletedDetailsMapper()
        {
            CreateMap<CompletedDetails, CompletedDetailsDto>();
        }
    }
}