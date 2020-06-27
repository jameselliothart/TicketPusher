using TicketPusher.API.Common;
using TicketPusher.DataTransfer.Projects;

namespace TicketPusher.API.Projects.Queries
{
    public class GetProjectListQuery : GetEntityListQuery<ProjectDto>
    {
        public GetProjectListQuery() : base()
        {
        }
    }
}