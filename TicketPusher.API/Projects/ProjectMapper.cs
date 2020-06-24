using AutoMapper;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(
                    dest => dest.ParentProject,
                    opt => opt.MapFrom(src => src.ParentProject.Id)
                );
        }
    }
}