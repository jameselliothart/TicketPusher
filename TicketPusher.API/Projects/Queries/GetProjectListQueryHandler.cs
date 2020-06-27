using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TicketPusher.API.Common;
using TicketPusher.API.Data;
using TicketPusher.DataTransfer.Projects;
using TicketPusher.Domain.Projects;

namespace TicketPusher.API.Projects.Queries
{
    public class GetProjectListQueryHandler : GetEntityListQueryHandler<GetProjectListQuery, ProjectDto, Project>
    {
        public GetProjectListQueryHandler(ITicketPusherRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }

        protected override async Task<List<Project>> GetEntitiesListAsync()
        {
            return await _repository.GetProjectsListAsync();
        }
    }
}