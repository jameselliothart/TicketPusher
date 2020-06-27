using AutoMapper;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(
                    dest => dest.ParentProjectId,
                    opt => opt.MapFrom(src => src.ParentProject.Id)
                );
        }
    }
}