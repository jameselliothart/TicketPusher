using System;

namespace TicketPusher.DataTransfer.Projects
{
    public class UpdateProjectDto
    {
        public string Name { get; set; }

        public Guid ParentProject { get; set; }
    }
}