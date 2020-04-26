using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPusher.API.Tickets;

namespace TicketPusher.Server.Data
{
    public class TicketService
    {
        public Task<IEnumerable<TicketDto>> GetTicketsAsync()
        {
            var tickets = Enumerable.Range(0, 3).Select(i => 
            {
                return new TicketDto
                {
                    Id = Guid.NewGuid(),
                    Owner = "Unassigned",
                    ProjectId = Guid.NewGuid(),
                    TicketDetails = new TicketDetailsDto()
                    {
                        Description = $"Ticket {i}",
                        SubmitDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(i),
                    },
                };
            });
            return Task.FromResult(tickets);
        }
    }
}
