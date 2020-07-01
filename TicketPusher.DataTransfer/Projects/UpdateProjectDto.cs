using System;
using System.ComponentModel.DataAnnotations;

namespace TicketPusher.DataTransfer.Projects
{
    public class UpdateProjectDto
    {
        public string Name { get; set; }

        public Guid ParentProjectId
        {
            get { return Guid.Parse(_parentProjectIdAsString); }
            set { _parentProjectIdAsString = value.ToString(); }
        }

        [Required]
        [Display(Name = "Parent Project")]
        public string _parentProjectIdAsString { get; set; }
    }
}