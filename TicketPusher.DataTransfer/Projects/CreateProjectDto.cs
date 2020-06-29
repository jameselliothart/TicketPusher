using System;
using System.ComponentModel.DataAnnotations;

namespace TicketPusher.DataTransfer.Projects
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }

        public Guid ParentProjectId => Guid.Parse(_parentProjectIdAsString);

        [Required]
        [Display(Name = "Parent Project")]
        public string _parentProjectIdAsString { get; set; }
    }
}