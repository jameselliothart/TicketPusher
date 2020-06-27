using System;
using System.ComponentModel.DataAnnotations;

namespace TicketPusher.DataTransfer.Projects
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }

        public Guid ParentProject { get; set; }
    }
}