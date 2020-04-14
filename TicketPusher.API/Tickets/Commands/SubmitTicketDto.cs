using System;
using System.ComponentModel.DataAnnotations;

namespace TicketPusher.API.Tickets.Commands
{
    public class SubmitTicketDto
    {
        [Required]
        public string Owner { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}