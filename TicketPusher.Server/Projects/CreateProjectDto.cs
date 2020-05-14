using System.ComponentModel.DataAnnotations;

namespace TicketPusher.Server.Projects
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }
    }
}