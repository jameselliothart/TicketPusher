using System;
using System.ComponentModel.DataAnnotations;
using TicketPusher.Domain.SharedKernel;

namespace TicketPusher.Server.Tickets
{
    public class SubmitTicketDto
    {
        [Required]
        public string Owner { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = NoSetDate.Instance;
        public Guid ProjectId => Guid.Parse(_projectIdAsString);

        [Required]
        [Display(Name = "Project")]
        public string _projectIdAsString { get; set; }
    }
}