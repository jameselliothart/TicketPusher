using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{

    public class TicketPusherRepository : ITicketPusherRepository, IDisposable
    {
        private readonly TicketPusherContext _context;
        private readonly NpgsqlConnection _reads;

        public TicketPusherRepository(TicketPusherContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            var readConnectionString = configuration.GetConnectionString("TicketPusherDb");
            _reads = new NpgsqlConnection(readConnectionString);
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));
            if (ticket.Id == Guid.Empty)
                throw new ArgumentException($"Ticket Id {ticket.Id} cannot be empty");

            _context.Tickets.Add(ticket);
        }

        public Ticket GetTicket(Guid ticketId)
        {
            return _context.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();
        }

        public List<Ticket> GetAllTickets()
        {
            return _context.Tickets.ToList();
        }

        public void RemoveTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}