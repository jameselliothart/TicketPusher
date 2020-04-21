using AutoMapper;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tickets
{
    public class TicketMapper : Profile
    {
        public TicketMapper()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.TicketDetails.Description))
                .ForMember(
                    dest => dest.SubmitDate,
                    opt => opt.MapFrom(src => src.TicketDetails.SubmitDate))
                .ForMember(
                    dest => dest.DueDate,
                    opt => opt.MapFrom(src => src.TicketDetails.DueDate))
                .ForMember(
                    dest => dest.ProjectId,
                    opt => opt.MapFrom(src => src.Project.Id))
                ;
        }
    }
}