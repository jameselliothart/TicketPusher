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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}