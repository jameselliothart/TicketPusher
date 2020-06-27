using System;
using TicketPusher.API.Common;
using TicketPusher.DataTransfer.Projects;

namespace TicketPusher.API.Projects.Queries
{
    public class GetProjectQuery : GetEntityQuery<ProjectDto>
    {
        public GetProjectQuery(Guid id) : base(id)
        {
        }
    }
}