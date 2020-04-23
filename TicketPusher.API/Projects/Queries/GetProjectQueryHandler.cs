using System;
using System.Threading.Tasks;
using AutoMapper;
using TicketPusher.API.Common;
using TicketPusher.API.Data;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects.Queries
{
    public class GetProjectQueryHandler : GetEntityQueryHandler<GetProjectQuery, ProjectDto, Project>
    {
        public GetProjectQueryHandler(ITicketPusherRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
        internal override async Task<Project> GetEntityAsync(Guid id) =>
            await _repository.GetProjectAsync(id);
    }
}