using AutoMapper;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}