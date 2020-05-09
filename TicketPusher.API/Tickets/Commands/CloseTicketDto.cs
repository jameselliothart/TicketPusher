using System.ComponentModel.DataAnnotations;

namespace TicketPusher.API.Tickets.Commands
{
    public class CloseTicketDto
    {
        public string Resolution { get; set; } = string.Empty;
    }
}