using AutoMapper;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.Domain.CompletedTickets;

namespace TicketPusher.API.CompletedTickets
{
    public class CompletedTicketsMapper : Profile
    {
        public CompletedTicketsMapper()
        {
            CreateMap<CompletedTicket, CompletedTicketDto>()
                .ForMember(
                    dest => dest.ProjectId,
                    opt => opt.MapFrom(src => src.Project.Id))
                ;
        }
    }
}