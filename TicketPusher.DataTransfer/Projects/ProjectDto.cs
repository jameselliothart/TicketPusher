using System;

namespace TicketPusher.DataTransfer.Projects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentProjectId { get; set; }
    }
}