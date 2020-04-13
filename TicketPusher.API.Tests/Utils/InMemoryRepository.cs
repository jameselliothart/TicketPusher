using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPusher.API.Data;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Tests.Utils
{
    internal class InMemoryRepository : ITicketPusherRepository
    {
        private readonly List<Ticket> _tickets = new List<Ticket>();
        public void CreateTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public Task<List<Ticket>> GetAllTicketsAsync()
        {
            return Task.FromResult(_tickets);
        }

        public Task<Ticket> GetTicketAsync(Guid ticketId)
        {
            return Task.FromResult(_tickets.Where(t => t.Id == ticketId).FirstOrDefault());
        }

        public void RemoveTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return true;
        }

        public Task<bool> SaveChangesAsync()
        {
            return Task.FromResult(true);
        }
    }
}