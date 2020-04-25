using TicketPusher.API.Common;

namespace TicketPusher.API.Projects.Queries
{
    public class GetProjectListQuery : GetEntityListQuery<ProjectDto>
    {
        public GetProjectListQuery() : base()
        {
        }
    }
}