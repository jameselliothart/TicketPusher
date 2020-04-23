using System.ComponentModel.DataAnnotations;

namespace TicketPusher.API.Projects.Commands
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }
    }
}