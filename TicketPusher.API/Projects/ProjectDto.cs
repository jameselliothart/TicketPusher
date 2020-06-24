using System;

namespace TicketPusher.API.Projects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentProject { get; set; }
    }
}